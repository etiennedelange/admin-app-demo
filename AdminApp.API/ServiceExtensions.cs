using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminApp.API.Options;
using AdminApp.API.Services;
using AdminApp.Data;
using AdminApp.Data.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace AdminApp.API.ServiceExtensions
{
	public static class ServiceExtensions
	{
		public static readonly ILoggerFactory DebugLoggerFactory = LoggerFactory.Create(builder => builder.AddDebug());

		public static void Configure(this IServiceCollection services, IConfiguration configuration)
		{
			ConfigureDbContext(services, configuration.GetSection($"{ConnectionStringsOptions.ConnectionStrings}").Get<ConnectionStringsOptions>());
			ConfigureEmailService(services);

			services.AddIdentity<ApplicationUser, ApplicationRole>(SetIdentityOptions)
				.AddRoleManager<RoleManager<ApplicationRole>>()
				.AddDefaultTokenProviders()
				.AddEntityFrameworkStores<AdminAppContext>()
				.AddTokenProvider<UserEmailConfirmationTokenProvider>(UserEmailConfirmationTokenProvider.NAME);

			services.AddHttpContextAccessor();

			services.AddHealthChecks();

			services.AddMvc()
				.AddNewtonsoftJson(options => options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local);

			services.AddSignalR((options) =>
			{
				// Timeout for automatically sending a ping message to keep the connection open
				options.KeepAliveInterval = TimeSpan.FromMinutes(10);
				options.EnableDetailedErrors = true;
			});

			//services.AddAuthorization(options =>
			//{
			//    // Blocks access to any resource when no authentication data is present
			//    //options.FallbackPolicy = new AuthorizationPolicyBuilder()
			//    //    .RequireAuthenticatedUser()
			//    //    .Build();

			//    //options.AddPolicy("HubMethod", policy =>
			//    //{
			//    //    policy.Requirements.Add(new HubMethodAuthorizeRequirement());
			//    //});
			//});
		}

		public static void ConfigureCors(this IServiceCollection services, string corsPolicyName, IEnumerable<string> allowedOrigins)
		{
			services.AddCors((options) =>
			{
				options.AddPolicy(corsPolicyName, builder =>
				{
					builder
						.WithOrigins(allowedOrigins.ToArray())
						.AllowAnyHeader()
						.AllowCredentials()
						.WithMethods("GET", "POST");
				});
			});
		}

		public static void ConfigureDbContext(this IServiceCollection services, ConnectionStringsOptions connectionStringsOptions)
		{
			services
				.AddDbContext<AdminAppContext>(opts => opts
							.UseSqlServer(connectionStringsOptions.DefaultConnection, b =>
							 {
								 b.EnableRetryOnFailure(3);
								 b.MigrationsAssembly("AdminApp.Data");
							 })
							.UseLoggerFactory(DebugLoggerFactory)
							.EnableSensitiveDataLogging())
			//.AddScoped(_ => new AdminAppContext(connectionStringsOptions.DefaultConnection))
			;
		}

		public static void ConfigureEmailService(this IServiceCollection services)
		{
			services.AddScoped<IEmailSendingService, EmailSendingService>();
			services.AddScoped<IEmailTemplateService, EmailTemplateService>();
		}

		public static void ConfigureEmailTokenProvider(this IServiceCollection services, EmailOptions emailOptions)
		{
			services.Configure<UserEmailConfirmationTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(emailOptions.EmailConfirmationTokenExpiryMinutes));
		}

		public static void ConfigureAuthentication(this IServiceCollection services, AuthenticationOptions authenticationOptions)
		{
			var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authenticationOptions.Secret));

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(options =>
				{
					options.SaveToken = true;
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidIssuer = authenticationOptions.Issuer,
						ValidAudience = authenticationOptions.Audience,
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = key,
						ClockSkew = TimeSpan.FromSeconds(5),
					};

					/// This is required for authenticating SignalR calls from within the same domain.
					/// Normally, the JWT token would be read from the request headers, but with SignalR, sending the access token in the query string is required due to
					/// a limitation in Browser APIs. This means the token has to be manually added to the context in order for authorization to work e.g. AuthorizeAttribute
					options.Events = new JwtBearerEvents
					{
						OnMessageReceived = context =>
						{
							var accessToken = context.Request.Query["access_token"];

							// If the request is for the SignalR hub, append token to context in order to access the hub marked with Authorize attribute
							var path = context.HttpContext.Request.Path;
							if (!string.IsNullOrWhiteSpace(accessToken) && (path.StartsWithSegments(Constants.SignalR.TemplatesEndpoint) || path.StartsWithSegments(Constants.SignalR.NotificationsEndpoint)))
							{
								context.Token = accessToken;
							}
							return Task.CompletedTask;
						}
					};
				});
		}

		public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddOptions<AppOptions>()
				.Bind(configuration.GetSection(AppOptions.Name))
				.ValidateDataAnnotations();

			services.AddOptions<EmailOptions>()
				.Bind(configuration.GetSection($"{AppOptions.Name}:{EmailOptions.Email}"))
				.ValidateDataAnnotations();

			services.AddOptions<AllowedOriginsOptions>()
				.Bind(configuration.GetSection(AllowedOriginsOptions.AllowedOrigins))
				.ValidateDataAnnotations();

			services.AddOptions<ConnectionStringsOptions>()
				.Bind(configuration.GetSection(ConnectionStringsOptions.ConnectionStrings))
				.ValidateDataAnnotations();

			services.AddOptions<AuthenticationOptions>()
				.Bind(configuration.GetSection($"{AppOptions.Name}:{AuthenticationOptions.Authentication}"))
				.ValidateDataAnnotations();

			services.AddOptions<AvailableReportsOptions>()
				.Bind(configuration.GetSection(AvailableReportsOptions.AvailableReports))
				.ValidateDataAnnotations();
		}

		private static void SetIdentityOptions(IdentityOptions options)
		{
			options.SignIn.RequireConfirmedEmail = true;
			options.Tokens.EmailConfirmationTokenProvider = UserEmailConfirmationTokenProvider.NAME;
			//options.User.RequireUniqueEmail = true;// causes invalid email when registering!?!
		}
	}
}