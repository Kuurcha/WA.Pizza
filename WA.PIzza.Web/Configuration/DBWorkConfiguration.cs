using Microsoft.EntityFrameworkCore;
using Wa.Pizza.Infrasctructure.Data.Services;
using Wa.Pizza.Infrasctructure.Services;

namespace WA.PIzza.Web.Configuration
{
    public class DBWorkConfiguration
    {
        public static void configureDBWork(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
            builder.Services.AddScoped<OrderDataService>();
            builder.Services.AddScoped<BasketDataService>();
            builder.Services.AddScoped<CatalogDataService>();
        }
    }
}
