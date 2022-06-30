using Serilog;

namespace WA.PIzza.Web.Configuration
{
    public class CustomLoggerConfiguration
    {
        public static void configureLogger(WebApplicationBuilder builder)
        {
            var logger = new global::Serilog.LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            Log.Logger = logger;

            Log.Information("Application is starting up...");
            builder.Host.UseSerilog(logger);

            builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSeq();
            });
        }
    }
}
