using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Logging.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((context, logging) =>
                {
                    // CreateDefaultBuilder default logging configuration tasks to the returned Microsoft.Extensions.Hosting.HostBuilder:
                    //     • Configure the Microsoft.Extensions.Logging.ILoggerFactory to log to the following logging providers:
                    //          - Console (When running with Kestrel),
                    //          - Debug (VS Debug Output Window) and 
                    //          - Event source output

                    // Here is what the ASP.NET Core Framework does for us out of the box
                    // First clear all logging providers that CreateDefaultBuilder includes for us
                    // That means clear out everybody that is listening for logging events.
                    logging.ClearProviders();

                    // Add the "Logging" section of our configuration
                    logging.AddConfiguration(context.Configuration.GetSection("Logging"));

                    // Now lets add the logging providers (things we want to send logs to) that we wish to enable for our application
                    // For our demo we will just enable Debug & Console logging providers
                    logging.AddDebug();
                    logging.AddConsole();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
