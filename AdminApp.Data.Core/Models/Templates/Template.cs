using AdminApp.Common.EntityFramework.ChangeTracking;
using AdminApp.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminApp.Data.Models
{
    public class Template : DomainModel, IEntityChange
    {
        public Template()
        {
            DesktopProductVersions = new List<DesktopProductVersion>();
            OnlineProductVersions = new List<OnlineProductVersion>();
        }
        public string Description { get; set; }
        public Guid Guid { get; set; }
        public bool Available { get; set; }
        public TemplateData TemplateData { get; set; }
        public DateTime DateUploaded { get; set; }
        public DateTime DateLastUpdated { get; set; }

        public virtual ICollection<DesktopProductVersion> DesktopProductVersions { get; set; }
        public virtual ICollection<OnlineProductVersion> OnlineProductVersions { get; set; }

        #region IEntityChange members

        [NotMapped]
        public string EntityDescription => Description;

        #endregion
    }
}
