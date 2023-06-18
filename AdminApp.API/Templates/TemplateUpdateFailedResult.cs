using System;

namespace AdminApp.API.Templates
{
    public class TemplateUpdateFailedResult
    {
        public string Message { get; set; }
        public Guid TemplateGuid { get; set; }
        public string TemplateContentHash { get; set; }
    }
}
