using AdminApp.API;
using AdminApp.API.Services;
using AdminApp.API.Templates;
using AdminApp.Common.Services.Interfaces;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AdminApp.IntegrationTests
{
    public class MockWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
                {
                    services.AddSignalR((options) => options.EnableDetailedErrors = true);
                    services.AddSingleton<IPolicyEvaluator, MockPolicyEvaluator>();
                    services.AddSingleton<IErrorLoggingService, MockErrorLogger>();
                    services.AddSingleton<INotificationService, MockNotificationService>();
                })
                .Configure(app =>
                {
                    app.UseRouting();
                    app.UseAuthorization();
                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapHub<TemplatesHub>(Constants.SignalR.TemplatesEndpoint);
                    });
                });
        }

        protected override IHostBuilder CreateHostBuilder()
        {
            var builder = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(x => x.UseStartup<MockStartup>().UseTestServer());

            return builder;
        }
    }
}
