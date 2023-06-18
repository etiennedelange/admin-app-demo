using AdminApp.API.ViewModels;
using AdminApp.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using AdminApp.Data;

namespace AdminApp.API.Controllers
{
    [Authorize]
    public class NotificationsController : CrudApiControllerBase<TemplateNotificationViewModel, Notification>
    {
        public NotificationsController(AdminAppContext db) : base(db)
        { }

        protected override IQueryable<TemplateNotificationViewModel> Select(IQueryable<Notification> query)
        {
            return query.Select(notification => new TemplateNotificationViewModel
            {
                Id = notification.Id,
                Message = notification.Message,
                DateReceived = notification.DateReceived,
                Read = notification.Read ?? false,
                TemplateContentHash = notification.Template != null ? notification.Template.TemplateData.ContentHash : null,
                TemplateGuid = notification.Template != null ? notification.Template.Guid.ToString() : null,
                TemplateId = notification.TemplateId
            });
        }

        [HttpPost]
        public async Task<ActionResult<int>> Add(TemplateNotificationViewModel notificationViewModel)
        {
            var notification = new Notification
            {
                Message = notificationViewModel.Message,
                TemplateId = notificationViewModel.TemplateId
            };

            DbContext.Notification.Add(notification);

            return await DbContext.SaveChangesAsync();
        }

        [HttpPost, Route("removeAll")]
        public async ValueTask RemoveAll()
        {
            DbContext.Notification.RemoveRange(DbContext.Notification);

            await DbContext.SaveChangesAsync();
        }
    }
}