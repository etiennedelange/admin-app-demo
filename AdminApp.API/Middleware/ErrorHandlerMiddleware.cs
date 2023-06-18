using AdminApp.Common.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Data.SqlClient;
using System.Net;
using System.Threading.Tasks;

namespace AdminApp.API
{
    internal class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IErrorLoggingService _errorLogger;

        public ErrorHandlerMiddleware(RequestDelegate next, IErrorLoggingService errorLogger)
        {
            _errorLogger = errorLogger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _errorLogger.LogError(ex);

                await UpdateResponseAsync(httpContext, ex);
            }
        }

        private static Task UpdateResponseAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string errorMessage = exception?.InnerException?.InnerException switch
            {
                SqlException { Number: 547 } => "The operation cannot be completed because the item is in use",
                SqlException { Message: var message } => message,
                _ => exception.Message
            };

            return context.Response.WriteAsync(errorMessage);
        }
    }
}
