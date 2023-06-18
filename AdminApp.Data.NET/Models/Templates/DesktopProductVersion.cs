//using AdminApp.Common.EntityFramework.ChangeTracking;
//using AdminApp.Data.Common;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace AdminApp.Data.Models
//{
//    public class DesktopProductVersion : DomainModel, IEntityChange
//    {
//        public DesktopProductVersion()
//        {
//            Templates = new List<Template>();
//        }

//        [MaxLength(10)]
//        public string VersionNumber { get; set; }
//        public DateTime ReleaseDate { get; set; }
//        public ICollection<Template> Templates { get; set; }

//        #region IEntityChange members

//        [NotMapped]
//        public string EntityDescription => VersionNumber;

//        #endregion
//    }
//}
