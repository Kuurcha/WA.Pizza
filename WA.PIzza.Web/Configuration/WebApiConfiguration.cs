using FluentValidation.AspNetCore;
using Wa.Pizza.Infrasctructure.Validators;

namespace WA.PIzza.Web.Configuration
{
    public class WebApiConfiguration
    {
        public static void configurateWebApi(WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();
            builder.Services.AddControllers();
            builder.Services.AddControllers().AddFluentValidation(options =>
            {
                options.AutomaticValidationEnabled = true;
                options.RegisterValidatorsFromAssemblyContaining<BasketItemValidator>();
            });

        }
    }
}
