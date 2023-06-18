using AdminApp.API.Services;
using AdminApp.Data.Models;
using System;
using System.Threading.Tasks;

namespace AdminApp.IntegrationTests
{
    internal class MockNotificationService : INotificationService
    {
        public Task<Notification> AddAsync(string message)
        {
            throw new NotImplementedException();
        }

        public Task<Notification> AddAsync(string message, long? templateId)
        {
            throw new NotImplementedException();
        }
    }
}
