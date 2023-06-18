using System;
using System.Collections.Generic;

namespace AdminApp.Data.Models
{
    public class Setting : DomainModel
    {
        public Guid Guid { get; set; }
        public string Description { get; set; }
        public bool? Enabled { get; set; }

        public ICollection<AttorneySetting> AttorneySettings { get; set; }
    }
}