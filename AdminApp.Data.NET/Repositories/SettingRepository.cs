//using AdminApp.Data.Models;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Threading.Tasks;

//namespace AdminApp.Data.NET.Repositories
//{
//    public class SettingRepository : RepositoryBase<Setting>, ISettingRepository
//    {
//        public static async Task<IEnumerable<Guid>> GetActiveSettingsForAttorney(Guid attorneyKref)
//        {
//            using (var db = CreateDbContext())
//            {
//                // Globally enabled settings
//                List<Guid> activeSettings = await GetGlobalActiveSettings(db);

//                // Settings active per attorney
//                List<Guid> activeAttorneySettings = await GetActiveAttorneySettings(attorneyKref, db);

//                activeSettings.AddRange(activeAttorneySettings);

//                return new HashSet<Guid>(activeSettings);
//            }
//        }

//        public static async Task<bool> IsSettingActive(Guid attorneyKref, Guid settingGuid)
//        {
//            using (var db = CreateDbContext())
//            {
//                List<Guid> globalActiveSettings = await GetGlobalActiveSettings(db);
//                if (globalActiveSettings.Any(x => x == settingGuid))
//                {
//                    return true;
//                }
//                else
//                {
//                    List<Guid> attorneySettings = await GetActiveAttorneySettings(attorneyKref, db);
//                    return attorneySettings.Any(x => x == settingGuid);
//                }
//            }
//        }

//        private static async Task<List<Guid>> GetActiveAttorneySettings(Guid attorneyKref, AdminAppContext db)
//        {
//            return await (db.Attorney
//                .Where(x => x.Kref == attorneyKref)
//                .SelectMany(x => x.Settings.Select(a => a.Setting.Guid))).ToListAsync();
//        }

//        /// <summary>
//        /// GetActiveSettings returns Globally Active settings
//        /// </summary>
//        private static async Task<List<Guid>> GetGlobalActiveSettings(AdminAppContext db)
//        {
//            return await (db.Setting
//                            .Where(x => x.EnabledGlobally == true)
//                            .Select(x => x.Guid)).ToListAsync();
//        }

//        public static async Task<bool> GetGlobalActiveSettingAsync(Guid guid)
//        {
//            using (var db = CreateDbContext())
//            {
//                return await (db.Setting
//                            .Where(x => x.Guid == guid && x.EnabledGlobally == true)
//                            .AnyAsync());
//            }
//        }
//    }

//    public interface ISettingRepository : IRepository<Setting>
//    {

//    }
//}
