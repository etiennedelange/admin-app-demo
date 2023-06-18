using AdminApp.API.Helpers;
using AdminApp.API.Services;
using AdminApp.API.SignalR;
using AdminApp.API.ViewModels;
using AdminApp.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApp.API.Controllers
{
    public class AuthenticationController : ApiControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSendingService _emailService;

        public AuthenticationController(
            IAuthenticationService authService,
            UserManager<ApplicationUser> userManager,
            IEmailSendingService emailService)
        {
            _authService = authService;
            _userManager = userManager;
            _emailService = emailService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationUserViewModel>> Login([FromBody] LoginViewModel model)
        {
            var authenticatedUser = await _authService.AuthenticateAsync(model.Username, model.Password);

            if (authenticatedUser == null)
            {
                return Unauthorized("Invalid username or password");
            }

            return Ok(authenticatedUser);
        }

        /// <summary>
        /// Authenticates the SignalR user and returns the token to be used when accessing the SignalR hub.
        /// </summary>
        /// <param name="request">The login request containing the username and password of the SignalR user</param>
        /// <returns>The JWT token to be used when accessing the SignalR hub.</returns>
        [AllowAnonymous]
        [HttpPost("signalRLogin")]
        public async Task<string> SignalRLogin([FromBody] SignalRLoginRequest request)
        {
            AuthenticationUserViewModel authenticatedSignalRUser = await _authService.AuthenticateAsync(request.Username, request.Password);

            return authenticatedSignalRUser?.Token;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegistrationViewModel registrationViewModel)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByIdAsync(registrationViewModel.Id);

                string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                IdentityResult passwordResetResult = await _userManager.ResetPasswordAsync(user, passwordResetToken, registrationViewModel.Password);

                if (!passwordResetResult.Succeeded)
                {
                    return BadRequest(passwordResetResult.Errors.Select(x => x.Description));
                }
            }
            catch
            {
                return BadRequest("There was an error registering the user");
            }

            return Ok();
        }

        [HttpPost("changepassword")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordViewModel changePasswordModel)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(changePasswordModel.Username);

            if (user == null)
            {
                return NotFound($"User {changePasswordModel.Username} not found");
            }

            IdentityResult result = await _userManager.ChangePasswordAsync(user, changePasswordModel.CurrentPassword, changePasswordModel.NewPassword);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors?.First()?.Description ?? "Couldn't change password");
            }

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("sendresetpasswordtoken")]
        public async Task<ActionResult> SendResetPasswordToken([FromBody] ResetPasswordViewModel passwordResetModel)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(passwordResetModel.Username);

            if (user == null)
            {
                return NotFound($"User {passwordResetModel.Username} not found");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            await _emailService.SendPasswordResetEmailAsync(user, TokenHelper.Encode(token));

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("resetpassword")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordViewModel passwordResetModel)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(passwordResetModel.Id);

            // find way to reuse register/change password/forgot password code
            if (user == null)
            {
                return NotFound($"User {passwordResetModel.Username} not found");
            }

            var result = await _userManager.ResetPasswordAsync(user, TokenHelper.Decode(passwordResetModel.Token), passwordResetModel.NewPassword);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors?.First()?.Description ?? "Couldn't reset password");
            }

            return Ok();
        }
    }
}
