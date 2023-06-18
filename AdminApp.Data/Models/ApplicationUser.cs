using Microsoft.AspNetCore.Identity;

namespace AdminApp.Models
{
    public class ApplicationUser : IdentityUser<long>
    {
        public string FullName { get; set; }
        public bool IsActive { get; set; }
    }
}
