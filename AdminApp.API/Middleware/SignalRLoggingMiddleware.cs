using AdminApp.Common.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AdminApp.API
{
    public class SignalRLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IErrorLoggingService _errorLogger;

        public SignalRLoggingMiddleware(RequestDelegate next, IErrorLoggingService errorLogger)
        {
            _next = next;
            _errorLogger = errorLogger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            finally
            {
                if ((context.Request?.Path.StartsWithSegments(Constants.SignalR.TemplatesEndpoint)).GetValueOrDefault())
                {
                    if (context.Response?.StatusCode == (int)HttpStatusCode.Unauthorized)
                    {
                        _errorLogger.LogError(new Exception($"{context.Response?.StatusCode}: Authentication failed while trying to access {(context.Request?.Path.Value ?? "SignalR Hub")}. Either no token exists on the context or the token is invalid."));
                    }
                    else if (context.Response?.StatusCode == (int)HttpStatusCode.Forbidden)
                    {
                        _errorLogger.LogError(new Exception($"{context.Response?.StatusCode}: Authorization failed for user {context?.User?.Identity?.Name}. The requirements were not met. Please check associated user roles."));
                    }
                }
            }
        }
    }
}
