using System;
using System.Collections.Generic;

namespace AdminApp.API.ViewModels
{
    public class TemplateViewModel : IDomainViewModel
    {
        public TemplateViewModel()
        { }

        public long Id { get; set; }
        public Guid Guid { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
        public string TemplateContentHash { get; set; }

        public IEnumerable<ProductVersionViewModel> DesktopProductVersions { get; set; }
        public IEnumerable<ProductVersionViewModel> OnlineProductVersions { get; set; }
    }
}
