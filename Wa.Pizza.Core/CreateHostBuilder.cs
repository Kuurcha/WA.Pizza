using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
public class HostBuilder
{
    public static IHostBuilder CreateHostBuilder(string[] args)
        => Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(
                webBuilder => webBuilder.UseStartup<Startup>());
}

