//using AdminApp.Data.Models;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Threading.Tasks;

//namespace AdminApp.Data.NET.Repositories
//{
//    public class TemplateRepository : RepositoryBase<Template>, ITemplateRepository
//    {
//        public static async Task<Dictionary<Guid, string>> GetAvailableDesktopTemplateHashesByVersion(string versionNumber)
//        {
//            using (var db = CreateDbContext())
//            {
//                var templates = await db.Template
//                    .Where(x => x.Available && (x.DesktopProductVersions.Select(v => v.VersionNumber).Contains(versionNumber)))
//                    .Select(d => new { d.Guid, d.TemplateData.ContentHash })
//                    .ToDictionaryAsync(x => x.Guid, x => x.ContentHash);

//                return templates;
//            }
//        }

//        public static async Task<Dictionary<Guid, string>> GetAvailableOnlineTemplateHashesByVersion(string versionNumber)
//        {
//            using (var db = CreateDbContext())
//            {
//                var templates = await db.Template
//                    .Where(x => x.Available && (x.OnlineProductVersions.Select(v => v.VersionNumber).Contains(versionNumber)))
//                    .Select(d => new { d.Guid, d.TemplateData.ContentHash })
//                    .ToDictionaryAsync(x => x.Guid, x => x.ContentHash);

//                return templates;
//            }
//        }

//        public static async Task<(byte[] content, string contentHash)> GetTemplateDataByGuid(Guid guid)
//        {
//            using (var db = CreateDbContext())
//            {
//                var templateData = await db.Template
//                    .Where(x => x.Guid == guid)
//                    .Select(x => new { x.TemplateData.Content, x.TemplateData.ContentHash })
//                    .FirstOrDefaultAsync();

//                return (templateData.Content, templateData.ContentHash);
//            }
//        }
//    }

//    public interface ITemplateRepository : IRepository<Template>
//    { }
//}
