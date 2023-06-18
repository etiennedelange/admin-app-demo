using AdminApp.Data.Models;
using MimeKit;
using System.Threading.Tasks;

namespace AdminApp.API.Services
{
    public interface IEmailTemplateService
    {
        ValueTask BuildActivationEmailMessageContent(MimeMessage mailMessage, ApplicationUser user, string encodedToken);
        ValueTask BuildResetEmailMessageContent(MimeMessage mailMessage, ApplicationUser user, string encodedToken);
    }
}
