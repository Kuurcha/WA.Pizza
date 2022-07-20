using WA.PIzza.Web.Middleware;

namespace WA.PIzza.Web.Extensions
{
    /// <summary>
    /// IApplicationBuilder custom exceptions using
    /// </summary>
    public static class ExceptionMiddlewareExtensions
    {
        /// <summary>
        /// Tells IApplcationBuilder to use ExceptionMiddleware 
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureCustomExceptionhandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
