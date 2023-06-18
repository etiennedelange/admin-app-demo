using System;
using System.Collections.Generic;

namespace AdminApp.API.ViewModels
{
    public class AttorneyViewModel : IDomainViewModel
    {
        public AttorneyViewModel()
        { }

        public long Id { get; set; }
        public string Name { get; set; }
        public Guid Kref { get; set; }
        public bool Active { get; set; }
        public string LUN { get; set; }
        public string ALTLUN { get; set; }
        public string DebtorCode { get; set; }
        public string Branch { get; set; }
        public bool OnlineActivationChecked { get; set; }
        public DateTime? OnlineActivationDate { get; set; }
        public bool IsHostedFirm { get; set; }
        public IEnumerable<SettingViewModel> Settings { get; set; }
    }
}
