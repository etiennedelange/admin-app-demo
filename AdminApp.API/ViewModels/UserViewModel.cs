using System.Collections.Generic;

namespace AdminApp.API.ViewModels
{
    public class UserViewModel : IDomainViewModel
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public IList<string> Roles { get; set; }
    }
}
