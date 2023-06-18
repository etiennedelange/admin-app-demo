using AdminApp.API.ViewModels;
using System.Threading.Tasks;

namespace AdminApp.API.Notifications
{
    public interface INotificationsHubClient
    {
        /// <summary>
        /// Adds the notification to the side panel and shows a toast message if the panel is closed
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        Task Notify(TemplateNotificationViewModel notification);

        /// <summary>
        /// Adds the notification to the side panel and shows a toast message if the panel is closed
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        Task Notify(NotificationViewModel notification);

        /// <summary>
        /// Shows a toast notification client-side
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        Task Toast(NotificationViewModel notification);
    }
}