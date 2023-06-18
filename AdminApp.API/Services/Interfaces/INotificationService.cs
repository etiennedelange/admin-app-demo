using AdminApp.Data.Models;
using System.Threading.Tasks;

namespace AdminApp.API.Services
{
    public interface INotificationService
    {
        Task<Notification> AddAsync(string message);
        Task<Notification> AddAsync(string message, long? templateId);
    }
}
