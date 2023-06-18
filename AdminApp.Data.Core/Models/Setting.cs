using AdminApp.Common.EntityFramework.ChangeTracking;
using AdminApp.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminApp.Data.Models
{
    public class Setting : DomainModel, IEntityChange
    {
        public Guid Guid { get; set; }
        public string Description { get; set; }
        public bool? EnabledGlobally { get; set; }

        public virtual ICollection<AttorneySetting> Attorneys { get; set; }

        #region IEntityChange members

        [NotMapped]
        public string EntityDescription => Description;

        #endregion
    }
}