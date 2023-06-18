using AdminApp.API.SignalR;
using AdminApp.Common.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace AdminApp.API.Notifications
{
    [Authorize]
    public class NotificationsHub : HubBase<INotificationsHubClient>
    {
        public NotificationsHub(IErrorLoggingService errorLogger)
            : base(errorLogger)
        { }
    }
}
