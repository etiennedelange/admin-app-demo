using Microsoft.AspNetCore.Identity;

namespace AdminApp.Models
{
    public class ApplicationRole : IdentityRole<long>
    {
        public ApplicationRole()
        {
        }

        public ApplicationRole(string roleName)
            : base(roleName)
        { }
    }
}
