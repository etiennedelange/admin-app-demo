using AdminApp.Common.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AdminApp.API.SignalR
{
    public abstract class HubBase<T> : Hub<T>
        where T : class
    {
        private readonly IErrorLoggingService _errorLogger;

        public HubBase(IErrorLoggingService errorLogger) => _errorLogger = errorLogger;

        public override Task OnConnectedAsync()
        {
            Debug.WriteLine($"{GetType()?.Name} connected");

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Debug.WriteLine("TemplatesHub disconnected");

            if (exception != null)
            {
                _errorLogger.LogError($"{GetType()?.Name} disconnected", exception);
            }

            return base.OnDisconnectedAsync(exception);
        }
    }
}
