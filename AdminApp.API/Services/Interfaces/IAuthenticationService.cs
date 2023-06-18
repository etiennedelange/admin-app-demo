using AdminApp.API.ViewModels;
using System.Threading.Tasks;

namespace AdminApp.API.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticationUserViewModel> AuthenticateAsync(string userName, string password);
    }
}