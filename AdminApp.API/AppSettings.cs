namespace AdminApp.API
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TokenExpiryMinutes { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public bool EnableSsl { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public string EmailSenderName { get; set; }
        public string EmailSenderFromAddress { get; set; }
        public int EmailConfirmationTokenExpiryMinutes { get; set; }
    }
}
