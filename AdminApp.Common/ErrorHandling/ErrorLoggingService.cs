using AdminApp.Common.Services.Interfaces;
using NLog;
using System;

namespace AdminApp.Common
{
    public class ErrorLoggingService : IErrorLoggingService
    {
        private static readonly ILogger _logger = LogManager.LoadConfiguration("nlog.config").GetLogger("DatabaseErrorLog");

        public void LogError(string message, Exception exception)
        {
            _logger.Error(exception, message, exception);
        }

        public void LogError(Exception exception)
        {
            LogEventInfo eventInfo = new LogEventInfo(LogLevel.Trace, _logger.Name, exception.Message)
            {
                Exception = exception
            };
            eventInfo.Properties.Add("ProductVersion", GetType()?.Assembly?.GetName()?.Version?.ToString());

            _logger.Error(eventInfo);
        }

        public void LogError(string message)
        {
            LogError(new Exception(message));
        }
    }
}
