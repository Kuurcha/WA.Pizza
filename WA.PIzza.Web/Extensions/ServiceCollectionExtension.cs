using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using Wa.Pizza.Infrasctructure.Data.Services;
using Wa.Pizza.Infrasctructure.Services;
using Wa.Pizza.Infrasctructure.Validators;

namespace WA.PIzza.Web.Extensions
{
    public static class ServiceCollectionExtension
    {
       public static void injectServices (this IServiceCollection services)
        {
            services.AddScoped<OrderDataService>();
            services.AddScoped<BasketDataService>();
            services.AddScoped<CatalogDataService>();
        }
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
            });
        }
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
        public static void configureWeb(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddControllers().AddFluentValidation(options =>
            {
                options.AutomaticValidationEnabled = true;
                options.RegisterValidatorsFromAssemblyContaining<BasketItemValidator>();
            });
        }

        public static void configureDBContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
