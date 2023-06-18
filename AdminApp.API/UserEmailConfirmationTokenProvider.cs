using AdminApp.Data.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AdminApp.API
{
    internal class UserEmailConfirmationTokenProvider : DataProtectorTokenProvider<ApplicationUser>
    {
        internal static string NAME = "emailConfirmationTokenProvider";

        public UserEmailConfirmationTokenProvider(
            IDataProtectionProvider dataProtectionProvider,
            IOptionsSnapshot<UserEmailConfirmationTokenProviderOptions> options,
            ILogger<DataProtectorTokenProvider<ApplicationUser>> logger)
            : base(dataProtectionProvider, options, logger)
        { }
    }
}
