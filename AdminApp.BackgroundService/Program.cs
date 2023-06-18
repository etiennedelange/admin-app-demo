using AdminApp.BackgroundService.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace AdminApp.BackgroundService
{
    public class Program
    {
        private const string EVENT_LOG_NAME = "Admin Application";
        private const string SOURCE_NAME = "Background Service";

        public static void Main(string[] args)
        {
            if (OperatingSystem.IsWindows())
            {
                if (!EventLog.SourceExists(EVENT_LOG_NAME))
                {
                    EventLog.CreateEventSource(SOURCE_NAME, EVENT_LOG_NAME);
                }
            }

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService((WindowsServiceLifetimeOptions options) => options.ServiceName = SOURCE_NAME)
                .ConfigureServices((HostBuilderContext hostBuilderContext, IServiceCollection services) =>
                {
                    IConfiguration configuration = hostBuilderContext.Configuration;

                    services.AddOptions<ConnectionStringsOptions>()
                        .Bind(configuration.GetSection(ConnectionStringsOptions.ConnectionStrings))
                        .ValidateDataAnnotations();

                    services.AddOptions<BackgroundWorkerOptions>()
                        .Bind(configuration.GetSection(BackgroundWorkerOptions.BackgroundWorker))
                        .ValidateDataAnnotations();
                })
                .ConfigureLogging((ILoggingBuilder logging) => logging.AddEventLog(logging =>
                {
                    logging.LogName = EVENT_LOG_NAME;
                    logging.SourceName = SOURCE_NAME;
                }))
                .ConfigureServices((hostContext, services) => services.AddHostedService<BackgroundWorker>());
    }
}
