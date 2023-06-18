using AdminApp.API.ViewModels;
using AdminApp.Data;
using AdminApp.Data.Models;
using System.Linq;

namespace AdminApp.API.Controllers
{
    public class AuditLogController : CrudApiControllerBase<AuditLogViewModel, AuditLog>
    {
        public AuditLogController(AdminAppContext db)
            : base(db)
        { }

        protected override IQueryable<AuditLog> Filter(IQueryable<AuditLog> query, int pageIndex, int pageSize, string filter, string sortOrder)
        {
            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(a => a.Data.Contains(filter)
                || a.User.Contains(filter));
            }

            if (sortOrder == "asc")
            {
                query = query.OrderBy(a => a.Id);
            }
            else
            {
                query = query.OrderByDescending(a => a.Id);
            }

            return query;
        }

        protected override IQueryable<AuditLogViewModel> Select(IQueryable<AuditLog> page)
        {
            return page.Select(auditLog => new AuditLogViewModel
            {
                Id = auditLog.Id,
                User = auditLog.User,
                Date = auditLog.Date,
                Data = auditLog.Data
            });
        }
    }
}