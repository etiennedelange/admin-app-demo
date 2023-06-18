using AdminApp.Common.EntityFramework.ChangeTracking;
using AdminApp.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminApp.Data.Models
{
    public class Attorney : DomainModel, IEntityChange
    {
        public Attorney()
        {
            Settings = new List<AttorneySetting>();
        }

        public string Name { get; set; }
        public Guid Kref { get; set; }
        public bool Active { get; set; }
        public string LUN { get; set; }
        public string ALT_LUN { get; set; }
        public string DebtorCode { get; set; }
        public string Branch { get; set; }
        public bool OnlineActivationChecked { get; set; }
        public DateTime? OnlineActivationDate { get; set; }
        public bool IsHostedFirm { get; set; }

        public virtual ICollection<AttorneySetting> Settings { get; set; }

        #region IEntityChange members

        [NotMapped]
        public string EntityDescription => Name;

        #endregion
    }
}