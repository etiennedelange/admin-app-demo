using AdminApp.API.Options;
using AdminApp.API.ViewModels;
using AdminApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AdminApp.API.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthenticationOptions _authenticationOptions;

        public AuthenticationService(
            UserManager<ApplicationUser> userManager,
            IOptionsSnapshot<AuthenticationOptions> authenticationOptions,
            RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _authenticationOptions = authenticationOptions?.Value;
        }

        public async Task<AuthenticationUserViewModel> AuthenticateAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            ApplicationUser user = await _userManager.FindByNameAsync(username);

            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authenticationOptions.Secret));


            //
            //var certStore = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            //certStore.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            //// Query the store for the thumbprint and get a result (zero or one result in collectiion) 
            //var certificateCollection = certStore.Certificates.Find(X509FindType.FindByThumbprint, "113320b09de2f4e3ea7cfd28dfd432e4eebe62f5", false);
            //X509Certificate2 cert = certificateCollection[0];
            //var key = new X509SecurityKey(cert);
            //

            ClaimsIdentity claims = await GetClaimsIdentity(user);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = claims,
                Expires = DateTime.Now.AddMinutes(_authenticationOptions.TokenExpiryMinutes),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                Audience = _authenticationOptions.Audience,
                Issuer = _authenticationOptions.Issuer
            };

            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

            var userRoles = await _userManager.GetRolesAsync(user);

            return new AuthenticationUserViewModel(username, user.FullName, tokenHandler.WriteToken(token), userRoles);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(ApplicationUser user)
        {
            var claims = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            });

            foreach (var userRole in await _userManager.GetRolesAsync(user))
            {
                claims.AddClaim(new Claim(ClaimTypes.Role, userRole));
            }

            return claims;
        }
    }
}