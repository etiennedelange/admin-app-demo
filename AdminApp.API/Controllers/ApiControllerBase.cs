using AdminApp.Data;
using AdminApp.Data.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AdminApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected ApiControllerBase()
        { }

        protected ApiControllerBase(AdminAppContext db) => DbContext = db;

        protected AdminAppContext DbContext { get; }

        protected class DomainModelIdComparer<T> : IEqualityComparer<T>
            where T : DomainModel
        {
            public bool Equals(T x, T y) => x.Id == y.Id;

            public int GetHashCode(T obj) => obj.Id.GetHashCode();
        }

        protected class DomainModelIdComparer : DomainModelIdComparer<DomainModel>
        { }
    }
}