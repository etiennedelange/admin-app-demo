using System;

namespace AdminApp.API.ViewModels
{
    public class AuditLogViewModel : IDomainViewModel
    {
        public AuditLogViewModel()
        { }

        public long Id { get; set; }
        public string User { get; set; }
        public DateTime Date { get; set; }
        public string Data { get; set; }
    }
}
