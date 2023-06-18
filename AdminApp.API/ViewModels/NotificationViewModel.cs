using System;

namespace AdminApp.API.ViewModels
{
    public class NotificationViewModel : IDomainViewModel
    {
        public NotificationViewModel()
        {
            DateReceived = DateTime.Now;
            Read = false;
        }

        public static implicit operator NotificationViewModel(string message) => new() { Message = message };

        public long Id { get; set; }
        public string Message { get; set; }
        public DateTime DateReceived { get; set; }
        public bool Read { get; set; }
    }
}
