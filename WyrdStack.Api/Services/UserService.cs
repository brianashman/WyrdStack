using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WyrdStack.Api.Models.Dtos;
using WyrdStack.Api.Models.Dtos.Users.Request;

namespace WyrdStack.Api.Services
{
	public class UserService : IUserService
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public UserService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public async Task<List<IdentityUser>> GetAllAsync() => await _userManager.Users.ToListAsync();

		public async Task<IdentityUser?> GetAsync(string id) => await _userManager.FindByIdAsync(id);

		public async Task<IdentityResult> CreateAsync(IdentityUser user, string password)
		{
			var result = await _userManager.CreateAsync(user, password);
			if (result.Succeeded)
			{
				// Ensure the "User" role exists in the DB before assigning it
				string defaultRole = "User";
				if (!await _roleManager.RoleExistsAsync(defaultRole))
				{
					await _roleManager.CreateAsync(new IdentityRole(defaultRole));
				}

				// Assign default role safely
				await _userManager.AddToRoleAsync(user, defaultRole);
			}
			return result;
		}

		public async Task<bool> DeleteAsync(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user is null) return false;

			var result = await _userManager.DeleteAsync(user);
			return result.Succeeded;
		}

		public async Task<IdentityResult> UpdateAsync(string id, UpdateUserDTO dto)
		{
			var existingUser = await _userManager.FindByIdAsync(id);
			if (existingUser is null)
				return IdentityResult.Failed(new IdentityError { Description = "User not found." });

			if (!string.IsNullOrWhiteSpace(dto.UserName) && dto.UserName != existingUser.UserName)
			{
				var setUserNameResult = await _userManager.SetUserNameAsync(existingUser, dto.UserName);
				if (!setUserNameResult.Succeeded) return setUserNameResult;
			}

			if (!string.IsNullOrWhiteSpace(dto.Email) && dto.Email != existingUser.Email)
			{
				var setEmailResult = await _userManager.SetEmailAsync(existingUser, dto.Email);
				if (!setEmailResult.Succeeded) return setEmailResult;
			}

			return IdentityResult.Success;
		}

		public async Task<IdentityResult> ChangePasswordAsync(string id, string oldPassword, string newPassword)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user is null)
				return IdentityResult.Failed(new IdentityError { Description = "User not found." });

			return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
		}
	}
}