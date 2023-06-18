using System;
using System.Threading.Tasks;
using AdminApp.Common.Services.Interfaces;
using AdminApp.Data;
using AdminApp.Data.Models;
using Microsoft.AspNetCore.Http;

namespace AdminApp.API.Services
{
	public class AuditLoggingService : IAuditLoggingService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly AdminAppContext _adminAppDbContext;

		public AuditLoggingService(
			IHttpContextAccessor httpContextAccessor,
			AdminAppContext adminAppDbContext)
		{
			_httpContextAccessor = httpContextAccessor;
			_adminAppDbContext = adminAppDbContext;
		}

		/// <summary>
		/// Logs a JSON string containing the details of the change.
		/// </summary>
		/// <param name="change">The JSON string containing the details of the change.</param>
		/// <param name="user">The current user</param>
		/// <returns></returns>
		public async ValueTask LogAsync(string change) => await LogAsync(change, _httpContextAccessor.HttpContext?.User?.Identity?.Name);

		/// <summary>
		/// Logs a JSON string containing the details of the change.
		/// </summary>
		/// <param name="change">The JSON string containing the details of the change.</param>
		/// <returns></returns>
		public async ValueTask LogAsync(string change, string user)
		{
			if (string.IsNullOrWhiteSpace(change))
			{
				return;
			}

			AuditLog auditLog = new()
			{
				Date = DateTime.Now,
				Data = change,
				User = !string.IsNullOrWhiteSpace(user) ? user : "Unknown"
			};

			_adminAppDbContext.AuditLog.Add(auditLog);

			await _adminAppDbContext.SaveChangesAsync();
		}
	}
}
