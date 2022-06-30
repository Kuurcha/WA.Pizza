using Microsoft.OpenApi.Models;
using System.Reflection;

namespace WA.PIzza.Web.Configuration
{
    public class SwaggerConfiguration
    {
        public static void configureSwagger(WebApplicationBuilder webApplicationBuilder)
        {
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var fullPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);

            webApplicationBuilder.Services.AddSwaggerGen(options =>
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
    }
}
