using AdminApp.API.Notifications;
using AdminApp.API.Services;
using AdminApp.API.SignalR;
using AdminApp.API.ViewModels;
using AdminApp.Common.Services.Interfaces;
using AdminApp.Data;
using AdminApp.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApp.API.Templates
{
    [Authorize(Roles = "SignalR")]
    public class TemplatesHub : HubBase<ITemplatesHubClient>
    {
        private readonly IHubContext<NotificationsHub, INotificationsHubClient> _notificationsHubContext;
        private readonly INotificationService _notificationService;
        private readonly AdminAppContext _dbContext;

        private static readonly HashSet<long> _connectedClientUserIds = new();

        public static HashSet<long> ConnectedClientUserIds => new(_connectedClientUserIds);

        public TemplatesHub(
            IErrorLoggingService errorLogger,
            IHubContext<NotificationsHub, INotificationsHubClient> notificationsHubContext,
            INotificationService notificationService,
            AdminAppContext dbContext)
            : base(errorLogger)
        {
            _notificationsHubContext = notificationsHubContext;
            _notificationService = notificationService;
            _dbContext = dbContext;
        }

        public async Task Echo(string message)
        {
            await Clients.Caller.Echo(message);
        }

        public async Task TemplateUpdateSuccessAsync(string message)
        {
            await _notificationsHubContext.Clients.All.Notify(new TemplateNotificationViewModel { Message = message });

            await _notificationService.AddAsync(message);
        }

        public async Task TemplateUpdateErrorAsync(TemplateUpdateFailedResult result)
        {
            // Make this more efficient, send TemplateId with request from LCO?
            long? templateId = await _dbContext.Template
                .Where(x => x.Guid == result.TemplateGuid)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            Notification notification = await _notificationService.AddAsync(result.Message, templateId);

            await _notificationsHubContext.Clients.All.Notify(new TemplateNotificationViewModel
            {
                Id = notification.Id,
                Message = result.Message,
                TemplateContentHash = result.TemplateContentHash,
                TemplateGuid = result.TemplateGuid.ToString(),
                TemplateId = templateId,
                DateReceived = notification.DateReceived,
                Read = false
            });
        }

        public override Task OnConnectedAsync()
        {
            Debug.WriteLine($"{Context.User?.Identity?.Name} connected.");

            if (TryGetClientUserId(out long userId))
            {
                _connectedClientUserIds.Add(userId);
            }

            _notificationsHubContext.Clients.All.Toast("Connected.");

            return base.OnConnectedAsync();
        }

        private bool TryGetClientUserId(out long userId) => long.TryParse(Context.UserIdentifier, out userId) && userId != default;

        public override Task OnDisconnectedAsync(Exception exception)
        {
            if (TryGetClientUserId(out long userId))
            {
                _connectedClientUserIds.Remove(userId);
            }

            _notificationsHubContext.Clients.All.Toast("Disconnected.");

            return base.OnDisconnectedAsync(exception);
        }
    }
}