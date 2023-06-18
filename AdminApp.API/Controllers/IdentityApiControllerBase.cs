using AdminApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public abstract class IdentityApiControllerBase : ControllerBase
    {
        protected IdentityApiControllerBase()
        { }

        protected IdentityApiControllerBase(AdminAppContext db) => IdentityDbContext = db;

        protected AdminAppContext IdentityDbContext { get; }
    }
}
