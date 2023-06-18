using AdminApp.API.DbContextExtensions;
using AdminApp.API.ViewModels;
using AdminApp.Data;
using AdminApp.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApp.API.Controllers
{
    public class DesktopProductVersionsController : CrudApiControllerBase<ProductVersionViewModel, DesktopProductVersion>
    {
        public DesktopProductVersionsController(AdminAppContext db)
            : base(db)
        { }

        protected override IQueryable<DesktopProductVersion> Filter(IQueryable<DesktopProductVersion> query, int pageIndex, int pageSize, string filter, string sortOrder)
        {
            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(a => a.VersionNumber.Contains(filter));
            }

            if (sortOrder == "asc")
            {
                query = query.OrderBy(a => a.VersionNumber);
            }
            else
            {
                query = query.OrderByDescending(a => a.VersionNumber);
            }

            return query;
        }

        protected override IQueryable<ProductVersionViewModel> Select(IQueryable<DesktopProductVersion> page)
        {
            return page
               .Select(template => new ProductVersionViewModel
               {
                   Id = template.Id,
                   ReleaseDate = template.ReleaseDate,
                   VersionNumber = template.VersionNumber
               });
        }

        [HttpPut]
        public async Task<ActionResult<int>> Save(ProductVersionViewModel productVersionViewModel)
        {
            var setting = await DbContext.DesktopProductVersion.FirstOrDefaultAsync(a => a.Id == productVersionViewModel.Id);

            if (setting == null)
            {
                return NotFound($"Product Version with ID {productVersionViewModel.Id} not found.");
            }

            UpdateProductVersion(productVersionViewModel, setting);

            return await DbContext.SaveChangesWithAuditAsync();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Add(ProductVersionViewModel productVersionViewModel)
        {
            var productVersion = new DesktopProductVersion();

            UpdateProductVersion(productVersionViewModel, productVersion);

            DbContext.DesktopProductVersion.Add(productVersion);

            return await DbContext.SaveChangesWithAuditAsync();
        }

        private static void UpdateProductVersion(ProductVersionViewModel productVersionViewModel, DesktopProductVersion productVersion)
        {
            productVersion.ReleaseDate = productVersionViewModel.ReleaseDate;
            productVersion.VersionNumber = productVersionViewModel.VersionNumber;
        }
    }
}

