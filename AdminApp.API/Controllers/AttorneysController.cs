using AdminApp.API.DbContextExtensions;
using AdminApp.API.ViewModels;
using AdminApp.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminApp.Data;
using Microsoft.EntityFrameworkCore;

namespace AdminApp.API.Controllers
{
    public class AttorneysController : CrudApiControllerBase<AttorneyViewModel, Attorney>
    {

        public AttorneysController(AdminAppContext db)
            : base(db)
        { }

        protected override IQueryable<Attorney> Filter(IQueryable<Attorney> query, int pageIndex, int pageSize, string filter, string sortOrder)
        {
            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(a => a.Name.Contains(filter)
                || a.LUN.Contains(filter)
                || a.Branch.Contains(filter)
                || a.DebtorCode.Contains(filter));
            }

            if (sortOrder == "asc")
            {
                query = query.OrderBy(a => a.Name);
            }
            else
            {
                query = query.OrderByDescending(a => a.Name);
            }

            return query;
        }

        protected override IQueryable<AttorneyViewModel> Select(IQueryable<Attorney> page)
        {
            return page.Select(attorney => new AttorneyViewModel
            {
                Active = attorney.Active,
                ALTLUN = attorney.ALT_LUN,
                Branch = attorney.Branch,
                DebtorCode = attorney.DebtorCode,
                Id = attorney.Id,
                Kref = attorney.Kref,
                LUN = attorney.LUN,
                Name = attorney.Name,
                OnlineActivationChecked = attorney.OnlineActivationChecked,
                OnlineActivationDate = attorney.OnlineActivationDate,
                IsHostedFirm = attorney.IsHostedFirm
            });
        }

        protected override IQueryable<AttorneyViewModel> SelectDetailed(IQueryable<Attorney> page)
        {
            return page.Select(attorney => new AttorneyViewModel
            {
                Active = attorney.Active,
                ALTLUN = attorney.ALT_LUN,
                Branch = attorney.Branch,
                DebtorCode = attorney.DebtorCode,
                Id = attorney.Id,
                Kref = attorney.Kref,
                LUN = attorney.LUN,
                Name = attorney.Name,
                OnlineActivationChecked = attorney.OnlineActivationChecked,
                OnlineActivationDate = attorney.OnlineActivationDate,
                IsHostedFirm = attorney.IsHostedFirm,
                Settings = attorney.Settings.Select(setting => new SettingViewModel
                {
                    Id = setting.Setting.Id,
                    Guid = setting.Setting.Guid,
                    Description = setting.Setting.Description,
                    EnabledGlobally = setting.Setting.EnabledGlobally
                })
            });
        }

        [HttpPut]
        public async Task<ActionResult<int>> Save(AttorneyViewModel attorneyViewModel)
        {
            var attorney = await DbContext.Attorney.FirstOrDefaultAsync(a => a.Id == attorneyViewModel.Id);

            if (attorney == null)
            {
                return NotFound($"Attorney with ID {attorneyViewModel.Id} not found.");
            }

            UpdateAttorney(attorneyViewModel, attorney);
            UpdateAttorneySettings(attorneyViewModel, attorney);

            return await DbContext.SaveChangesWithAuditAsync();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Add(AttorneyViewModel attorneyViewModel)
        {
            var attorney = new Attorney();

            UpdateAttorney(attorneyViewModel, attorney);

            DbContext.Attorney.Add(attorney);

            UpdateAttorneySettings(attorneyViewModel, attorney);

            return await DbContext.SaveChangesWithAuditAsync();
        }

        private static void UpdateAttorney(AttorneyViewModel attorneyViewModel, Attorney attorney)
        {
            attorney.Active = attorneyViewModel.Active;
            attorney.Branch = attorneyViewModel.Branch;
            attorney.DebtorCode = attorneyViewModel.DebtorCode;
            attorney.Kref = attorneyViewModel.Kref;
            attorney.LUN = attorneyViewModel.LUN;
            attorney.Name = attorneyViewModel.Name;
            attorney.ALT_LUN = attorneyViewModel.ALTLUN;
            attorney.DebtorCode = attorneyViewModel.DebtorCode;
            attorney.OnlineActivationDate = attorneyViewModel.OnlineActivationDate;
            attorney.OnlineActivationChecked = attorneyViewModel.OnlineActivationChecked;
            attorney.IsHostedFirm = attorneyViewModel.IsHostedFirm;
        }

        private static void UpdateAttorneySettings(AttorneyViewModel attorneyViewModel, Attorney attorney)
        {
            foreach (var setting in GetRemovedEntities(attorney.Id, attorney.Settings, attorneyViewModel.Settings))
            {
                attorney.Settings.Remove(setting);
            }

            foreach (var setting in GetAddedEntities(attorney.Id, attorneyViewModel.Settings, attorney.Settings))
            {
                attorney.Settings.Add(setting);
            }
        }

        private static IEnumerable<AttorneySetting> GetRemovedEntities(long attorneyId, IEnumerable<AttorneySetting> currentValues, IEnumerable<SettingViewModel> selectedValues)
        {
            return currentValues
                .Except(selectedValues.Select(x => new AttorneySetting { AttorneyId = attorneyId, SettingId = x.Id }), new AttorneySettingComparer())
                .ToList();
        }

        private static IEnumerable<AttorneySetting> GetAddedEntities(long attorneyId, IEnumerable<SettingViewModel> selectedValues, IEnumerable<AttorneySetting> currentValues)
        {
            return selectedValues.Select(x => new AttorneySetting { AttorneyId = attorneyId, SettingId = x.Id })
                .Except(currentValues, new AttorneySettingComparer())
                .ToList();
        }

        protected class AttorneySettingComparer : IEqualityComparer<AttorneySetting>
        {
            public bool Equals(AttorneySetting x, AttorneySetting y) => (x.AttorneyId == y.AttorneyId) && (x.SettingId == y.SettingId);

            public int GetHashCode(AttorneySetting obj) => obj.AttorneyId.GetHashCode() ^ obj.SettingId.GetHashCode();
        }
    }
}