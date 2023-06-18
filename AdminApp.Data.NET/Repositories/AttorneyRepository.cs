//using AdminApp.Data.Models;
//using System;
//using System.Data.Entity;
//using System.Threading.Tasks;

//namespace AdminApp.Data.NET.Repositories
//{
//    public class AttorneyRepository : RepositoryBase<Attorney>, IAttorneyRepository
//    {
//        public static async Task<Attorney> GetAttorneyDetails(Guid attorneyKref)
//        {
//            using (var db = CreateDbContext())
//            {
//                var attorney = await db.Attorney.FirstAsync(x => x.Kref == attorneyKref);

//                return attorney;
//            }
//        }
//    }

//    public interface IAttorneyRepository : IRepository<Attorney>
//    {
//    }
//}