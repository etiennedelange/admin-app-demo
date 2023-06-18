using AdminApp.API.DbContextExtensions;
using AdminApp.API.ViewModels;
using AdminApp.Data;
using AdminApp.Data.Common;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApp.API.Controllers
{
    public abstract class CrudApiControllerBase<TViewModel, TDomainModel> : ApiControllerBase
        where TViewModel : class, IDomainViewModel
        where TDomainModel : DomainModel, new()
    {
        protected CrudApiControllerBase(AdminAppContext db)
            : base(db)
        { }

        protected virtual IQueryable<TDomainModel> Filter(IQueryable<TDomainModel> query, int pageIndex, int pageSize, string filter, string sortOrder) => query;

        protected abstract IQueryable<TViewModel> Select(IQueryable<TDomainModel> query);

        /// <summary>
        /// Used to select more detailed results. If not overridden, the normal Select is returned.
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        protected virtual IQueryable<TViewModel> SelectDetailed(IQueryable<TDomainModel> page) => Select(page);

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TViewModel>>> Get(
            [FromQuery] int pageIndex,
            [FromQuery] int pageSize,
            [FromQuery] string filter,
            [FromQuery] string sortOrder = "asc")
        {
            var pageToSkip = pageIndex * pageSize;

            var query = DbContext.Set<TDomainModel>().AsQueryable();

            query = Filter(query, pageIndex, pageSize, filter, sortOrder);

            var pageQuery = query
                .Skip(pageToSkip)
                .Take(pageSize);

            return await Select(pageQuery).ToListAsync();
        }

        [Route("all"), HttpGet]
        public async Task<IEnumerable<TViewModel>> GetAll() => await Select(DbContext.Set<TDomainModel>()).ToListAsync();

        [HttpGet("total")]
        public virtual async Task<ActionResult<int>> GetTotal() => await DbContext.Set<TDomainModel>().CountAsync();

        [HttpGet("{id:int}")]
        public virtual async Task<ActionResult<TViewModel>> Get(long id)
        {
            var query = DbContext
                .Set<TDomainModel>()
                .Where(x => x.Id == id);

            return await SelectDetailed(query).FirstOrDefaultAsync();
        }

        [HttpDelete("{id:int}")]
        public virtual async Task<ActionResult<int>> Remove(long id)
        {
            var entity = await DbContext
                .Set<TDomainModel>()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity != null)
            {
                DbContext.Set<TDomainModel>().Remove(entity);
            }

            return await DbContext.SaveChangesWithAuditAsync();
        }

        protected static IEnumerable<TDomainModel> GetRemovedEntities(IEnumerable<TDomainModel> currentValues, IEnumerable<TViewModel> selectedValues)
        {
            return currentValues
                .Except(selectedValues.Select(x => new TDomainModel { Id = x.Id }), new DomainModelIdComparer<TDomainModel>())
                .ToList();
        }

        protected static IEnumerable<TDomainModel> GetAddedEntities(IEnumerable<TViewModel> selectedValues, IEnumerable<TDomainModel> currentValues)
        {
            return selectedValues.Select(x => new TDomainModel { Id = x.Id })
                .Except(currentValues, new DomainModelIdComparer<TDomainModel>())
                .ToList();
        }
    }
}
