using AdminApp.API.Notifications;
using AdminApp.API.Templates;
using AdminApp.API.ViewModels;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApp.API.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly IHubContext<TemplatesHub, ITemplatesHubClient> _templateHubContext;
        private readonly IHubContext<NotificationsHub, INotificationsHubClient> _notificationHubContext;

        public TemplateService(
            IHubContext<TemplatesHub, ITemplatesHubClient> templateHubContext,
            IHubContext<NotificationsHub, INotificationsHubClient> notificationHubContext)
        {
            _templateHubContext = templateHubContext;
            _notificationHubContext = notificationHubContext;
        }

        /// <summary>
        /// Notifies connected clients that it needs to download the template with the specified GUID
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="contentHash"></param>
        /// <returns></returns>
        public async Task TemplateUpdatedAsync(Guid guid, string contentHash, bool forceUpdate = false)
        {
            if (TemplatesHub.ConnectedClientUserIds.Any())
            {
                await _templateHubContext.Clients.All.TemplateUpdatedAsync(guid, contentHash, forceUpdate);
            }
            else
            {
                await _notificationHubContext.Clients.All.Notify(new TemplateNotificationViewModel
                {
                    Message = "No clients connected.",
                    TemplateContentHash = contentHash,
                    TemplateGuid = guid.ToString()
                });
            }
        }
    }
}
