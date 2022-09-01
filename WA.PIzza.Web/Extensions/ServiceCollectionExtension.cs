﻿using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text;
using Wa.Pizza.Infrasctructure.Data.Services;
using Wa.Pizza.Infrasctructure.Services;
using Wa.Pizza.Infrasctructure.Validators;
using Microsoft.AspNetCore.Builder;
using Hangfire;
using MediatR;
using static Wa.Pizza.Infrasctructure.Data.CQRS.Basket.BasketCommands;
using static BasketQueries;
using Wa.Pizza.Infrasctructure.Data.CQRS.Basket;
using Wa.Pizza.Infrasctructure.DTO.AdvertisementDTO;

namespace WA.PIzza.Web.Extensions
{
    /// <summary>
    /// Extension for setting up services 
    /// </summary>
    public static class ServiceCollectionExtension
    {
        public static void configureFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidation(options =>
             {
                 options.AutomaticValidationEnabled = true;
                 options.RegisterValidatorsFromAssemblyContaining<BasketItemValidator>();
                 options.RegisterValidatorsFromAssemblyContaining<CatalogItemValidator>();
                 options.RegisterValidatorsFromAssemblyContaining<UpdateOrderValidator>();
                 options.RegisterValidatorsFromAssemblyContaining<AddOrderValidator>();
                 options.RegisterValidatorsFromAssemblyContaining<BasketValidator>();
                 options.RegisterValidatorsFromAssemblyContaining<CreateAdvertisementClientValidator<CreateAdvertisementClientDTO>>();
                 options.RegisterValidatorsFromAssemblyContaining<UpdateDeleteAdvertisementClientValidator<UpdateDeleteAdvertisementClientDTO>>();
                 options.RegisterValidatorsFromAssemblyContaining<AdvertisementClientValidator>();
                 options.RegisterValidatorsFromAssemblyContaining<AdvertisementValidator>();

             });
        }
        public static void configureMediatR(this IServiceCollection services)
        {
            services.AddMediatR(typeof(BasketCommands));
            services.AddMediatR(typeof(BasketQueries));
        }
        public static void configureHangfire(this IServiceCollection services, string connectionString)
        {
            services.AddHangfire(configuration =>
            {
                configuration.UseSqlServerStorage(connectionString)
                .UseRecommendedSerializerSettings()
                .UseSimpleAssemblyNameTypeSerializer();
               
            });
            services.AddHangfireServer();

            
        }
        /// <summary>
        /// Injects user created services into the program
        /// </summary>
        /// <param name="services"></param>
        /// <param name="appMail"></param>
        /// <param name="password"></param>
        public static void injectServices (this IServiceCollection services, string appMail, string password)
        {
            services.AddScoped<OrderDataService>();
            services.AddScoped<CatalogDataService>();
            services.AddScoped<TokenService>();
            services.AddScoped<AuthenticationService>();
            services.AddScoped<ApiKeyService>();
            services.AddScoped<AdvertisementClientService>();
            services.AddScoped<AdvertisementService>();
            services.AddSingleton<SMTPService>(x => new SMTPService(appMail, password));
        }
        /// <summary>
        /// Injects and configures swagger
        /// </summary>
        /// <param name="services"></param>
        public static void configureSwagger(this IServiceCollection services)
        {
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var fullPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "An ASP.NET Core Web API for managing ToDo items",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Example Contact",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Example License",
                        Url = new Uri("https://example.com/license")
                    }
                });
                options.IncludeXmlComments(fullPath);
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter ‘Bearer’ [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\""
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}

                    }
                });
                

            });
        }
        /// <summary>
        /// Injects and configures logging with Seq and Serilog
        /// </summary>
        /// <param name="services"></param>
        /// <param name="builder"></param>
        public static void configureLogger(this IServiceCollection services, WebApplicationBuilder builder)
        {
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            Log.Logger = logger;

            builder.Host.UseSerilog(logger);
            builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSeq();
            });
        }
        /// <summary>
        /// Configures and injects controllers
        /// </summary>
        /// <param name="services"></param>
        public static void configureWeb(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddControllers();
        }
        /// <summary>
        /// Injects applicationDbContext
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        public static void configureDBContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        }
        /// <summary>
        /// Injects authentication, using JWT, identity.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="Configuration"></param>
        public static void ConfigureIdentity(this IServiceCollection services, ConfigurationManager Configuration)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }
            )
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
               
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidAudience = Configuration["JWT: ValidAudience"],
                    ValidIssuer = Configuration["JWT: ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT: SecretKey"]))
                };
            }
            );



        }
    }
}
