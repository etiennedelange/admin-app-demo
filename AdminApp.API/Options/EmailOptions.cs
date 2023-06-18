using System.ComponentModel.DataAnnotations;

namespace AdminApp.API.Options
{
    public record EmailOptions
    {
        public const string Email = "Email";

        //[Required(ErrorMessage = "SmtpHost is required in appSettings")]
        public string SmtpHost { get; init; }

        //[Required(ErrorMessage = "SmtpPort is required in appSettings")]
        public int SmtpPort { get; init; }

        public string SmtpUsername { get; init; }
        public string SmtpPassword { get; init; }
        public string EmailSenderName { get; init; }
        public string EmailSenderFromAddress { get; init; }
        public int EmailConfirmationTokenExpiryMinutes { get; init; }
    }
}
