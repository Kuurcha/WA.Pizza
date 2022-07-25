using Hangfire;
using Hangfire.Dashboard;
using WA.PIzza.Web.ReccuringJobs;

namespace WA.PIzza.Web.Extensions
{
    /// <summary>
    /// Extension to separate IApplicationBuilder usings to specific part of the system
    /// </summary>
    public static class ApplicationBuilderExtension
    {
        /// <summary>
        /// Use all networking parts of the system  
        /// </summary>
        /// <param name="appBuilder"></param>
        /// <param name="isDevelopment"></param>
        public static void useHttp(this IApplicationBuilder appBuilder, bool isDevelopment)
        {
            if (isDevelopment)
            {
                appBuilder.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                appBuilder.UseHsts();
            }


            appBuilder.UseHttpsRedirection();

            appBuilder.UseStaticFiles();

            appBuilder.UseRouting();

            appBuilder.UseAuthorization();

            appBuilder.ConfigureCustomExceptionhandler();

            appBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // подключаем маршрутизацию на контроллеры
                endpoints.MapHangfireDashboard(); 
            });
        }
        /// <summary>
        /// Specifies swagger's use
        /// </summary>
        /// <param name="appBuilder"></param>
        public static void useSwaggerWithUI(this IApplicationBuilder appBuilder)
        {
            appBuilder.UseSwagger();

            appBuilder.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }

        public static void useHangfireCustom(this IApplicationBuilder appBuilder)
        {
            var options = new DashboardOptions()
            {
                Authorization = new[] { new MyAuthorizationFilter() }
            };
            appBuilder.UseHangfireDashboard("/hangfire", options);
            RecurringJob.AddOrUpdate<ForgottenBasketJob>(
                "forgottenBasketJob",
                job => job.Run(),
                Cron.MinuteInterval(1));
        }

    }
    public class MyAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context) => true;
    }


}
