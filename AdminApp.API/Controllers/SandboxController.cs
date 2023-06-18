using AdminApp.API.Notifications;
using AdminApp.API.ViewModels;
using AdminApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AdminApp.API.Controllers
{
    public class SandboxController : ApiControllerBase
    {
        private readonly IHubContext<NotificationsHub, INotificationsHubClient> _notificationsHubContext;

        public SandboxController(
            AdminAppContext db,
            IHubContext<NotificationsHub, INotificationsHubClient> notificationsHubContext)
            : base(db)
        {
            _notificationsHubContext = notificationsHubContext;
        }

        [HttpPost, Route("postNotificationToHub")]
        public async Task<IActionResult> Add(NotificationViewModel notification)
        {
            await _notificationsHubContext.Clients.All.Notify(notification);

            return Ok();
        }
    }
}