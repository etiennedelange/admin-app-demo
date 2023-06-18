using AdminApp.Data.Models;
using System.Threading.Tasks;

namespace AdminApp.API.Services
{
    public interface IEmailSendingService
    {
        ValueTask SendActivationEmailAsync(ApplicationUser user, string encodedToken);
        ValueTask SendPasswordResetEmailAsync(ApplicationUser user, string encodedToken);
    }
}
