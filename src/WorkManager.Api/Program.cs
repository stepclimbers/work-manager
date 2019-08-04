using Loggly;
using Loggly.Config;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using WorkManager.Core.Settings;

namespace WorkManager.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.ClearProviders();
                })
                .UseSerilog((hostingContext, loggerConfiguration) =>
                {
                    var logglySettings = hostingContext.Configuration.GetSection(nameof(LogglySettings)).Get<LogglySettings>();
                    SetupLogglyConfiguration(logglySettings);

                    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
                });

        private static void SetupLogglyConfiguration(LogglySettings logglySettings)
        {
            // Configure Loggly
            var config = LogglyConfig.Instance;
            config.CustomerToken = logglySettings.CustomerToken;
            config.ApplicationName = logglySettings.ApplicationName;
            config.Transport = new TransportConfiguration()
            {
                EndpointHostname = logglySettings.EndpointHostname,
                EndpointPort = logglySettings.EndpointPort,
                LogTransport = logglySettings.LogTransport
            };
            config.ThrowExceptions = logglySettings.ThrowExceptions;

            // Define Tags sent to Loggly
            config.TagConfig.Tags.AddRange(new ITag[]
            {
                new ApplicationNameTag { Formatter = "Application-{0}" },
                new HostnameTag { Formatter = "Host-{0}" }
            });
        }
    }
}
