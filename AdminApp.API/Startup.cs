using AdminApp.API.Notifications;
using AdminApp.API.Options;
using AdminApp.API.ServiceExtensions;
using AdminApp.API.Services;
using AdminApp.API.Templates;
using AdminApp.Common;
using AdminApp.Common.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdminApp.API
{
	public class Startup
	{
		private readonly string CorsPolicyName = "AdminAppCorsPolicy";

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public virtual void ConfigureServices(IServiceCollection services)
		{
			services.ConfigureOptions(Configuration);
			services.Configure(Configuration);
			services.ConfigureEmailTokenProvider(Configuration.GetSection($"{AppOptions.Name}:{EmailOptions.Email}").Get<EmailOptions>());
			services.ConfigureCors(CorsPolicyName, Configuration.GetSection(AllowedOriginsOptions.AllowedOrigins).Get<AllowedOriginsOptions>());
			services.ConfigureAuthentication(Configuration.GetSection($"{AppOptions.Name}:{AuthenticationOptions.Authentication}").Get<AuthenticationOptions>());

			services.AddSingleton<IErrorLoggingService, ErrorLoggingService>();

			services.AddTransient<IAuditLoggingService, AuditLoggingService>();

			services.AddScoped<IAuthenticationService, AuthenticationService>();
			services.AddScoped<ITemplateService, TemplateService>();
			services.AddScoped<INotificationService, NotificationService>();
		}

		public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.Use(async (context, next) =>
			{
				await next();
				if (context.Response.StatusCode == 404)
				{
					context.Request.Path = new PathString("/");
					await next();
				}
			});

			app.ConfigureExceptionHandling();
			app.UseDefaultFiles();
			app.UseStaticFiles();
			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseCors(CorsPolicyName);
			app.UseMiddleware<SignalRLoggingMiddleware>();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();

				endpoints.MapHub<TemplatesHub>(Constants.SignalR.TemplatesEndpoint)
					.RequireAuthorization();

				endpoints.MapHub<NotificationsHub>(Constants.SignalR.NotificationsEndpoint)
					.RequireAuthorization();

				endpoints.MapHealthChecks("/health");
			});
		}
	}
}
