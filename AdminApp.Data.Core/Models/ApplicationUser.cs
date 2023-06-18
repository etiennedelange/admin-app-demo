using AdminApp.Common.EntityFramework.ChangeTracking;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminApp.Data.Models
{
    public class ApplicationUser : IdentityUser<long>, IEntityChange
    {
        public string FullName { get; set; }
        public bool IsActive { get; set; }

        #region IEntityChange members

        [NotMapped]
        public long EntityId => Id;

        [NotMapped]
        public string EntityDescription => UserName;

        [NotMapped]
        public string EntityName => GetType()?.Name;

        #endregion
    }
}
