using AdminApp.Common.EntityFramework.ChangeTracking;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminApp.Data.Models
{
    public class ApplicationUserRole : IdentityUserRole<long>, IEntityChange
    {
        #region IEntityChange members

        [NotMapped]
        public long EntityId => default;

        [NotMapped]
        public string EntityDescription => $"RoleId - {RoleId}; UserId - {UserId}";

        [NotMapped]
        public string EntityName => GetType()?.Name;

        #endregion
    }
}
