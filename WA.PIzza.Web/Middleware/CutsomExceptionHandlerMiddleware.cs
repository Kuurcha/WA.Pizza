using System.Net;

namespace WA.PIzza.Web.Middleware
{
    /// <summary>
    /// Middleware for globaly handling exception and returning them as responses at runtime
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next; // type is a function delegate that can process our HTTP requests.

        /// <summary>
        /// ExceptionMiddleware DI constructor (
        /// </summary>
        /// <param name="next"></param>
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        /// <summary>
        /// Catches exceptions, and either returns the controller's response, if no exceptions were throws, or calls HandleExceptionAsync
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        /// <summary>
        /// Writes exception as a JSON and returns it as response
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string exceptionMessage = exception.Message;
            var exceptionText = new { StatusCode = context.Response.StatusCode, Message = exceptionMessage };
            await context.Response.WriteAsJsonAsync(exceptionText);

        }
    }
}
