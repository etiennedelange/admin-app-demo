using System;

namespace AdminApp.API.ViewModels
{
    public class SettingViewModel : IDomainViewModel
    {
        public long Id { get; set; }
        public Guid Guid { get; set; }
        public string Description { get; set; }
        public bool? EnabledGlobally { get; set; }
    }
}
