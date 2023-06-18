using AdminApp.API.DbContextExtensions;
using AdminApp.API.Services;
using AdminApp.API.ViewModels;
using AdminApp.Data;
using AdminApp.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AdminApp.API.Controllers
{
    public class TemplatesController : CrudApiControllerBase<TemplateViewModel, Template>
    {
        private readonly ITemplateService _templateService;

        public TemplatesController(AdminAppContext db, ITemplateService templateService)
            : base(db)
        {
            _templateService = templateService;
        }

        protected override IQueryable<Template> Filter(IQueryable<Template> query, int pageIndex, int pageSize, string filter, string sortOrder)
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

        protected override IQueryable<TemplateViewModel> Select(IQueryable<Template> page)
        {
            return page
               .Select(template => new TemplateViewModel
               {
                   Id = template.Id,
                   Guid = template.Guid,
                   Description = template.Description,
                   Available = template.Available
               });
        }

        protected override IQueryable<TemplateViewModel> SelectDetailed(IQueryable<Template> page)
        {
            return page
               .Select(template => new TemplateViewModel
               {
                   Id = template.Id,
                   Guid = template.Guid,
                   Description = template.Description,
                   Available = template.Available,
                   TemplateContentHash = template.TemplateData.ContentHash,
                   DesktopProductVersions = template.DesktopProductVersions.Select(x => new ProductVersionViewModel { Id = x.Id, ReleaseDate = x.ReleaseDate, VersionNumber = x.VersionNumber }),
                   OnlineProductVersions = template.OnlineProductVersions.Select(x => new ProductVersionViewModel { Id = x.Id, ReleaseDate = x.ReleaseDate, VersionNumber = x.VersionNumber })
               });
        }

        // Might need to look at implementing streaming for large files - https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-5.0#upload-large-files-with-streaming
        [HttpPost]
        public async Task<ActionResult> Add([FromForm] IFormFile data)
        {
            if (data == null)
            {
                throw new InvalidDataException("No file data found.");
            }

            if (Request.Form == null || !Request.Form.TryGetValue("template", out StringValues value))
            {
                throw new InvalidDataException("No template model data found.");
            }

            var templateViewModel = JsonConvert.DeserializeObject<TemplateViewModel>(Request.Form["template"]);

            var template = new Template
            {
                TemplateData = await CreateTemplateDataFromFileAsync(data)
            };

            UpdateTemplate(templateViewModel, template);
            UpdateVersions(templateViewModel, template);

            template.DateUploaded = template.DateLastUpdated = DateTime.Now;

            DbContext.Template.Add(template);

            await DbContext.SaveChangesWithAuditAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<int>> Save(TemplateViewModel templateViewModel)
        {
            using var db = DbContext;

            var template = await db.Template
                .Include(x => x.TemplateData)// TODO: Remove this after testing
                .FirstOrDefaultAsync(a => a.Id == templateViewModel.Id);

            if (template == null)
            {
                return NotFound($"Template with ID {templateViewModel.Id} not found.");
            }

            UpdateTemplate(templateViewModel, template);
            UpdateVersions(templateViewModel, template);

            template.DateLastUpdated = DateTime.Now;

            return await DbContext.SaveChangesWithAuditAsync();
        }

        [HttpPut, Route("savewithtemplate")]
        public async Task<ActionResult<string>> SaveWithTemplate([FromForm] IFormFile data)
        {
            using var db = DbContext;

            var templateViewModel = JsonConvert.DeserializeObject<TemplateViewModel>(Request.Form["template"]);

            var template = await db.Template
                .Include(x => x.TemplateData)
                .FirstOrDefaultAsync(a => a.Id == templateViewModel.Id);

            if (template == null)
            {
                return NotFound($"Template with ID {templateViewModel.Id} not found.");
            }

            string currentContentHash = template.TemplateData.ContentHash;

            template.TemplateData = await CreateTemplateDataFromFileAsync(data);

            UpdateTemplate(templateViewModel, template);
            UpdateVersions(templateViewModel, template);

            template.DateLastUpdated = DateTime.Now;

            if (template.Available && currentContentHash != template.TemplateData.ContentHash)
            {
                await _templateService.TemplateUpdatedAsync(template.Guid, template.TemplateData.ContentHash);
            }

            await DbContext.SaveChangesWithAuditAsync();

            return Content(template.TemplateData.ContentHash);
        }

        [HttpGet, Route("desktopVersions")]
        public async Task<ActionResult<IEnumerable<ProductVersionViewModel>>> GetDesktopProductVersions()
        {
            return await DbContext.DesktopProductVersion
                .Select(x => new ProductVersionViewModel
                {
                    Id = x.Id,
                    VersionNumber = x.VersionNumber,
                    ReleaseDate = x.ReleaseDate
                }).ToListAsync();
        }

        [HttpGet, Route("onlineVersions")]
        public async Task<ActionResult<IEnumerable<ProductVersionViewModel>>> GetOnlineProductVersions()
        {
            return await DbContext.OnlineProductVersion
                .Select(x => new ProductVersionViewModel
                {
                    Id = x.Id,
                    VersionNumber = x.VersionNumber,
                    ReleaseDate = x.ReleaseDate
                }).ToListAsync();
        }

        [HttpPost, Route("queueDownload")]
        public async ValueTask QueueDownload([FromQuery] Guid guid, [FromQuery] string contentHash)
        {
            await _templateService.TemplateUpdatedAsync(guid, contentHash);
        }

        public override async Task<ActionResult<int>> Remove(long id)
        {
            var template = await DbContext.Template
                .Include(x => x.DesktopProductVersions)
                .Include(x => x.OnlineProductVersions)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (template != null)
            {
                template.TemplateData = new TemplateData() { Id = id };
                DbContext.Entry(template.TemplateData).State = EntityState.Deleted;

                DbContext.Template.Remove(template);
            }

            return await DbContext.SaveChangesWithAuditAsync();
        }

        private static async Task<TemplateData> CreateTemplateDataFromFileAsync(IFormFile file)
        {
            TemplateData templateData = new();

            using var stream = new MemoryStream();

            await file.CopyToAsync(stream);

            templateData.Content = stream.ToArray();

            if (templateData.Content == null || !templateData.Content.Any())
            {
                throw new InvalidDataException("No file data found.");
            }

            //templateData.ContentHash = TemplateHelper.GetMD5HashFromByteArray(templateData.Content);
            //templateData.Content = TemplateHelper.CompressContentData(templateData.Content);

            return templateData;
        }

        private static void UpdateTemplate(TemplateViewModel templateViewModel, Template template)
        {
            template.Description = templateViewModel.Description;
            template.Guid = templateViewModel.Guid;
            template.Available = templateViewModel.Available;
        }

        private void UpdateVersions(TemplateViewModel templateViewModel, Template template)
        {
            DesktopProductVersionsController.GetRemovedEntities(template.DesktopProductVersions, templateViewModel.DesktopProductVersions);

            foreach (var version in DesktopProductVersionsController.GetRemovedEntities(template.DesktopProductVersions, templateViewModel.DesktopProductVersions))
            {
                template.DesktopProductVersions.Remove(version);
            }

            foreach (var version in OnlineProductVersionsController.GetRemovedEntities(template.OnlineProductVersions, templateViewModel.OnlineProductVersions))
            {
                template.OnlineProductVersions.Remove(version);
            }

            foreach (var version in DesktopProductVersionsController.GetAddedEntities(templateViewModel.DesktopProductVersions, template.DesktopProductVersions))
            {
                DbContext.Entry(version).State = EntityState.Unchanged;
                DbContext.DesktopProductVersion.Attach(version);

                template.DesktopProductVersions.Add(version);
            }

            foreach (var version in OnlineProductVersionsController.GetAddedEntities(templateViewModel.OnlineProductVersions, template.OnlineProductVersions))
            {
                DbContext.Entry(version).State = EntityState.Unchanged;
                DbContext.OnlineProductVersion.Attach(version);

                template.OnlineProductVersions.Add(version);
            }
        }
    }
}

