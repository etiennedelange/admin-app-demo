namespace AdminApp.API.ViewModels
{
    public class TemplateNotificationViewModel : NotificationViewModel
    {
        public TemplateNotificationViewModel()
        { }

        public long? TemplateId { get; set; }
        public string TemplateContentHash { get; set; }
        public string TemplateGuid { get; set; }
    }
}
