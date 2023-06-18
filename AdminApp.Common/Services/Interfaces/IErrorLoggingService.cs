using System;

namespace AdminApp.Common.Services.Interfaces
{
    public interface IErrorLoggingService
    {
        void LogError(string message, Exception exception);
        void LogError(Exception exception);
        void LogError(string message);
    }
}
