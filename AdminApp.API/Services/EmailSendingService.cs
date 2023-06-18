using AdminApp.API.Options;
using AdminApp.Data.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net;
using System.Threading.Tasks;

namespace AdminApp.API.Services
{
    public class EmailTemplateModel
    {
        public MailboxAddress Username { get; set; }
        public string Url { get; set; }
        public string Image1ID { get; set; }
        public string Image2ID { get; set; }
    }

    public class EmailSendingService : IEmailSendingService
    {
        private readonly EmailOptions _emailOptions;

        private readonly IEmailTemplateService _emailTemplateHelper;

        private string SmtpHost => _emailOptions.SmtpHost;
        private int SmtpPort => _emailOptions.SmtpPort;

        private NetworkCredential Credentials => new(_emailOptions.SmtpUsername, _emailOptions.SmtpPassword);

        public EmailSendingService(
            IOptionsSnapshot<EmailOptions> emailOptions,
            IEmailTemplateService emailTemplateHelper)
        {
            _emailOptions = emailOptions.Value;
            _emailTemplateHelper = emailTemplateHelper;
        }

        private async Task<SmtpClient> GetConnectClient()
        {
            SmtpClient client = new();
            client.Connect(SmtpHost, SmtpPort, SecureSocketOptions.None);

            if (!string.IsNullOrWhiteSpace(Credentials.UserName) && !string.IsNullOrWhiteSpace(Credentials.Password))
            {
                await client.AuthenticateAsync(Credentials);
            }

            return client;
        }

        public async ValueTask SendActivationEmailAsync(ApplicationUser user, string encodedToken)
        {
            using (SmtpClient client = await GetConnectClient())
            {
                MimeMessage mailMessage = new();

                mailMessage.From.Add(MailboxAddress.Parse(_emailOptions.EmailSenderFromAddress));
                mailMessage.Subject = "Account Activation";
                mailMessage.To.Add(MailboxAddress.Parse(user.UserName));

                await _emailTemplateHelper.BuildActivationEmailMessageContent(mailMessage, user, encodedToken);

                await client.SendAsync(mailMessage);

                await client.DisconnectAsync(true);
            }
        }

        public async ValueTask SendPasswordResetEmailAsync(ApplicationUser user, string encodedToken)
        {
            using (SmtpClient client = await GetConnectClient())
            {
                MimeMessage mailMessage = new();

                mailMessage.From.Add(MailboxAddress.Parse(_emailOptions.EmailSenderFromAddress));
                mailMessage.Subject = "Password Reset";
                mailMessage.To.Add(MailboxAddress.Parse(user.UserName));

                await _emailTemplateHelper.BuildResetEmailMessageContent(mailMessage, user, encodedToken);

                await client.SendAsync(mailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
