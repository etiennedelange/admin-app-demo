using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminApp.Data.Models
{
    public class Attorney : DomainModel
    {
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

        public ICollection<AttorneySetting> AttorneySettings { get; set; }

        //[NotMapped]
        //public virtual ICollection<Setting> Settings { get; set; }
    }
}