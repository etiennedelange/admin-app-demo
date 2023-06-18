using AdminApp.Common.EntityFramework.ChangeTracking;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminApp.Data.Models
{
    public class ApplicationRole : IdentityRole<long>, IEntityChange
    {
        public ApplicationRole()
        {
        }

        public ApplicationRole(string roleName)
            : base(roleName)
        { }

        #region IEntityChange members

        [NotMapped]
        public long EntityId => Id;

        [NotMapped]
        public string EntityDescription => Name;

        [NotMapped]
        public string EntityName => GetType()?.Name;

        #endregion
    }
}
