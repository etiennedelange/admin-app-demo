using System;

namespace AdminApp.API.ViewModels
{
    public class ProductVersionViewModel : IDomainViewModel
    {
        public long Id { get; set; }
        public string VersionNumber { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
