using AdminApp.Data;
using AdminApp.Data.Models;
using System;
using System.Threading.Tasks;

namespace AdminApp.API.Services
{
    public class NotificationService : INotificationService
    {
        private readonly AdminAppContext _dbContext;

        public NotificationService(AdminAppContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Notification> AddAsync(string message) => await AddAsync(message, null);

        public async Task<Notification> AddAsync(string message, long? templateId)
        {
            var notification = new Notification
            {
                Message = message,
                TemplateId = templateId,
                DateReceived = DateTime.Now,
                Read = false
            };

            _dbContext.Notification.Add(notification);

            await _dbContext.SaveChangesAsync();

            return notification;
        }
    }
}
