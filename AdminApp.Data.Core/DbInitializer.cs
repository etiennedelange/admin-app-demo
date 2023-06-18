using AdminApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace AdminApp.Data.Core
{
    public static class DbInitializer
    {
        private static readonly PasswordHasher<ApplicationUser> _hasher = new();

        public async static Task Seed(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            await CreateAdminUser(userManager, roleManager);
            await CreatDefaultSignalRUser(userManager, roleManager);
        }

        private static async ValueTask CreateAdminUser(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            var userName = "admin@site.com";
            var user = new ApplicationUser
            {
                FullName = "Admin",
                UserName = userName,
                Email = "",
                AccessFailedCount = 0,
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                IsActive = true
            };
            user.PasswordHash = _hasher.HashPassword(user, "P@ssw0rd581");

            if (!await roleManager.RoleExistsAsync("Administrator"))
            {
                await roleManager.CreateAsync(new ApplicationRole("Administrator"));
            }

            if (!await roleManager.RoleExistsAsync("Manage Attorneys"))
            {
                await roleManager.CreateAsync(new ApplicationRole("Manage Attorneys"));
            }

            if (!await roleManager.RoleExistsAsync("Manage Users"))
            {
                await roleManager.CreateAsync(new ApplicationRole("Manage Users"));
            }

            if (!await roleManager.RoleExistsAsync("View Attorneys"))
            {
                await roleManager.CreateAsync(new ApplicationRole("View Attorneys"));
            }

            if (!await roleManager.RoleExistsAsync("View Users"))
            {
                await roleManager.CreateAsync(new ApplicationRole("View Users"));
            }

            if (!await roleManager.RoleExistsAsync("View Templates"))
            {
                await roleManager.CreateAsync(new ApplicationRole("View Templates"));
            }

            if (!await roleManager.RoleExistsAsync("Manage Templates"))
            {
                await roleManager.CreateAsync(new ApplicationRole("Manage Templates"));
            }

            if (!await roleManager.RoleExistsAsync("SignalR"))
            {
                await roleManager.CreateAsync(new ApplicationRole("SignalR"));
            }

            if (await userManager.FindByNameAsync(userName) == null)
            {
                await userManager.CreateAsync(user);
                await userManager.AddToRoleAsync(user, "Administrator");
            }
        }

        private static async ValueTask CreatDefaultSignalRUser(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            string userRoleAndName = "SignalR";
            var signalRUser = new ApplicationUser
            {
                FullName = userRoleAndName,
                UserName = userRoleAndName,
                Email = "",
                AccessFailedCount = 0,
                EmailConfirmed = true,
                LockoutEnabled = false,
                TwoFactorEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                IsActive = true
            };
            signalRUser.PasswordHash = _hasher.HashPassword(signalRUser, "P@ssw0rd");

            if (await userManager.FindByNameAsync(userRoleAndName) == null)
            {
                await userManager.CreateAsync(signalRUser);
                await userManager.AddToRoleAsync(signalRUser, userRoleAndName);
            }
        }
    }
}
