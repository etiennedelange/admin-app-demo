using System.Threading.Tasks;
using AdminApp.Common.Services.Interfaces;
using AdminApp.Data;
using AdminApp.Data.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;

namespace AdminApp.API.DbContextExtensions
{
	public static class DbContextExtensions
	{
		/// <summary>
		/// Persists the changes to the database and creates an audit log entry
		/// </summary>
		/// <param name="dbContext"></param>
		/// <returns></returns>
		public async static Task<int> SaveChangesWithAuditAsync(this AdminAppContext dbContext)
		{
			await TryLogChangesAsync(dbContext);

			return await dbContext.SaveChangesAsync();
		}

		public static async ValueTask TryLogChangesAsync(this AdminAppContext dbContext)
		{
            HttpContext httpContext = new HttpContextAccessor().HttpContext;

            if (httpContext == null)
            {
                return;
            }

            EntityChangeSerializerCore entityChangeSerializer = new(dbContext);

            var auditLogger = httpContext.RequestServices.GetService<IAuditLoggingService>();

            await auditLogger.LogAsync(entityChangeSerializer.GetChangesAsJson(new StringEnumConverter()));
        }
	}
}
