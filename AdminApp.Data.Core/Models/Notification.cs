using AdminApp.Data.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminApp.Data.Models
{
    public class Notification : DomainModel
    {
        public string Message { get; set; }

        public bool? Read { get; set; }

        public DateTime DateReceived { get; set; }

        public long? TemplateId { get; set; }

        [ForeignKey("TemplateId")]
        public Template Template { get; set; }
    }
}
