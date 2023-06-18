using System.Collections.Generic;

namespace AdminApp.API.ViewModels
{
    public class AuthenticationUserViewModel
    {
        public AuthenticationUserViewModel(string username, string fullName, string token, IList<string> roles)
        {
            Username = username;
            Token = token;
            Roles = roles;
            FullName = fullName;
        }

        public string Username { get; private set; }
        public string FullName { get; private set; }
        public string Token { get; private set; }
        public IList<string> Roles { get; set; }
    }
}