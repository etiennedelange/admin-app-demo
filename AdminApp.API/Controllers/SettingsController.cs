using System.Linq;
using System.Threading.Tasks;
using AdminApp.API.DbContextExtensions;
using AdminApp.API.ViewModels;
using AdminApp.Data;
using AdminApp.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminApp.API.Controllers
{
	public class SettingsController : CrudApiControllerBase<SettingViewModel, Setting>
	{
		public SettingsController(AdminAppContext db) : base(db)
		{ }

		protected override IQueryable<Setting> Filter(IQueryable<Setting> query, int pageIndex, int pageSize, string filter, string sortOrder)
		{
			if (!string.IsNullOrWhiteSpace(filter))
			{
				query = query.Where(a => a.Description.Contains(filter));
			}

			if (sortOrder == "asc")
			{
				query = query.OrderBy(a => a.Description);
			}
			else
			{
				query = query.OrderByDescending(a => a.Description);
			}

			return query;
		}

		protected override IQueryable<SettingViewModel> Select(IQueryable<Setting> page)
		{
			return page
			   .Select(setting => new SettingViewModel
			   {
				   Guid = setting.Guid, 
				   Description = setting.Description,
				   Id = setting.Id,
				   EnabledGlobally = setting.EnabledGlobally
			   });
		}

		[HttpPut]
		public async Task<ActionResult<int>> Save(SettingViewModel settingViewModel)
		{
			var setting = await DbContext.Setting.FirstOrDefaultAsync(a => a.Id == settingViewModel.Id);

			if (setting == null)
			{
				return NotFound($"Setting with ID {settingViewModel.Id} not found.");
			}

			UpdateSetting(settingViewModel, setting);

			return await DbContext.SaveChangesWithAuditAsync();
		}

		[HttpPost]
		public async Task<ActionResult<int>> Add(SettingViewModel settingViewModel)
		{
			var setting = new Setting();

			UpdateSetting(settingViewModel, setting);

			DbContext.Setting.Add(setting);

			return await DbContext.SaveChangesWithAuditAsync();
		}

		private static void UpdateSetting(SettingViewModel settingViewModel, Setting setting)
		{
			setting.Description = settingViewModel.Description;
			setting.Guid = settingViewModel.Guid;
			setting.EnabledGlobally = settingViewModel.EnabledGlobally;
		}
	}
}
