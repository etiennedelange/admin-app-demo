using AdminApp.Common.Services.Interfaces;
using System;

namespace AdminApp.IntegrationTests
{
    internal class MockErrorLogger : IErrorLoggingService
    {
        public void LogError(string message, Exception exception)
        {

        }

        public void LogError(Exception exception)
        {

        }

        public void LogError(string message)
        {

        }
    }
}
