using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminApp.API.Helpers;
using AdminApp.API.Services;
using AdminApp.API.ViewModels;
using AdminApp.Common.Services.Interfaces;
using AdminApp.Data;
using AdminApp.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminApp.API.Controllers
{
	public class UsersController : IdentityApiControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<ApplicationRole> _roleManager;
		private readonly IEmailSendingService _emailService;
		private readonly IErrorLoggingService _errorLogger;

		public UsersController(
			AdminAppContext db,
			UserManager<ApplicationUser> userManager,
			RoleManager<ApplicationRole> roleManager,
			IEmailSendingService emailService,
			IErrorLoggingService errorLogger) : base(db)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_emailService = emailService;
			_errorLogger = errorLogger;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ApplicationUser>>> Get(
			[FromQuery] int pageIndex,
			[FromQuery] int pageSize,
			[FromQuery] string filter,
			[FromQuery] string sortOrder = "asc")
		{
			var pageToSkip = pageIndex * pageSize;

			var query = IdentityDbContext.Users.AsQueryable();

			if (!string.IsNullOrWhiteSpace(filter))
			{
				query = query.Where(a => a.FullName.Contains(filter)
									|| a.UserName.Contains(filter));
			}

			if (sortOrder == "asc")
			{
				query = query.OrderBy(a => a.FullName);
			}
			else
			{
				query = query.OrderByDescending(a => a.FullName);
			}

			return await query.Skip(pageToSkip).Take(pageSize).ToListAsync();
		}

		[Route("all"), HttpGet]
		public async Task<IEnumerable<ApplicationUser>> GetAll() => await IdentityDbContext.Users.ToListAsync();

		[HttpGet("total")]
		public async Task<ActionResult<int>> GetTotal() => await IdentityDbContext.Users.CountAsync();

		[HttpGet("{id:int}")]
		public async Task<ActionResult<UserViewModel>> Get(long id)
		{
			var user = await _userManager.FindByIdAsync(id.ToString());
			var roles = await _userManager.GetRolesAsync(user);

			return new UserViewModel
			{
				Id = user.Id,
				FullName = user.FullName,
				IsActive = user.IsActive,
				UserName = user.UserName,
				Roles = roles
			};
		}

		[HttpPut]
		public async Task<ActionResult> Save(UserViewModel userViewModel)
		{
			ApplicationUser user = await _userManager.FindByIdAsync(userViewModel.Id.ToString());

			if (user == null)
			{
				return NotFound($"User with ID {userViewModel.Id} not found.");
			}

			UpdateUserDetails(userViewModel, user);
			await UpdateUserRoles(userViewModel, user);

			await _userManager.UpdateAsync(user);

			return Ok();
		}

		[HttpPost]
		public async Task<ActionResult<int>> Add(UserViewModel userViewModel)
		{
			var user = new ApplicationUser();

			UpdateUserDetails(userViewModel, user);

			IdentityResult result = await _userManager.CreateAsync(user);

			if (result.Succeeded)
			{
				var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

				await UpdateUserRoles(userViewModel, user);

				await _emailService.SendActivationEmailAsync(user, TokenHelper.Encode(token));

				return Ok();
			}
			else
			{
				string errorMessage = result.Errors?.FirstOrDefault()?.Description ?? "Error adding user";
				_errorLogger.LogError(new Exception(errorMessage));

				return BadRequest(errorMessage);
			}
		}

		[AllowAnonymous]
		[HttpPost("activate")]
		public async Task<ActionResult> Activate(long id, string token)
		{
			ApplicationUser user = await _userManager.FindByIdAsync(id.ToString());

			if (user == null)
			{
				return NotFound($"User with ID {id} not found");
			}

			var decodedToken = TokenHelper.Decode(token);

			IdentityResult result = await _userManager.ConfirmEmailAsync(user, decodedToken);

			if (!result.Succeeded)
			{
				return BadRequest(result.Errors.Any() ? $"{result.Errors.First().Description} The user might have already been registered." : "Could not confirm email address");
			}

			return Ok();
		}

		[AllowAnonymous]
		[HttpGet("getusername/{id:int}")]
		public async Task<ResetPasswordViewModel> GetUsername(long id)
		{
			return await IdentityDbContext.Users
				.Where(x => x.Id == id)
				.Select(x => new ResetPasswordViewModel { Username = x.UserName })
				.FirstOrDefaultAsync();
		}

		[HttpDelete("{id:int}")]
		public virtual async Task<ActionResult> Remove(long id)
		{
			ApplicationUser user = await _userManager.FindByIdAsync(id.ToString());

			if (user == null)
			{
				return NotFound($"User with ID {id} not found.");
			}

			IdentityResult result = await _userManager.DeleteAsync(user);

			if (!result.Succeeded)
			{
				string errorMessage = result.Errors?.FirstOrDefault()?.Description ?? "Error deleting user";
				_errorLogger.LogError(new Exception(errorMessage));

				return BadRequest(errorMessage);
			}

			return Ok();
		}

		private static void UpdateUserDetails(UserViewModel userViewModel, ApplicationUser user)
		{
			user.IsActive = userViewModel.IsActive;
			user.UserName = userViewModel.UserName;
			user.FullName = userViewModel.FullName;
		}

		private async ValueTask UpdateUserRoles(UserViewModel userViewModel, ApplicationUser user)
		{
			var currentRoles = await _userManager.GetRolesAsync(user);

			await _userManager.RemoveFromRolesAsync(user, currentRoles.Except(userViewModel.Roles));
			await _userManager.AddToRolesAsync(user, userViewModel.Roles.Except(currentRoles));
		}
	}
}
