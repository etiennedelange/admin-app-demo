using System;

namespace AdminApp.Data.Models
{
    public class LoggedEvent : DomainModel
    {
        public string UpdatedBy { get; set; }
        public string UpdatedItem { get; set; }
        public string Action { get; set; }
        public string ActionDescription { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
