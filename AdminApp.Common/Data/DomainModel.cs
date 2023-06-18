using System.ComponentModel.DataAnnotations.Schema;

namespace AdminApp.Data.Common
{
    public class DomainModel
    {
        public long Id { get; set; }

        #region IEntityChange members

        [NotMapped]
        public long EntityId => Id;

        [NotMapped]
        public string EntityName => GetType()?.Name;

        #endregion
    }
}
