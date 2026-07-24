using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WyrdStack.Api.Services
{
	public class UserService : IUserService
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;
		public UserService(UserManager<IdentityUser> userManager)
		{
			_userManager = userManager;
		}
		public async Task<List<IdentityUser>> GetAllAsync() => await _userManager.Users.ToListAsync();
		public async Task<IdentityUser?> GetAsync(string id) => await _userManager.FindByIdAsync(id);

		public async Task<bool> CreateAsync(IdentityUser user, string password)
		{
			var result = await _userManager.CreateAsync(user, password);
			return result.Succeeded;
		}
		public async Task<bool> DeleteAsync(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user is null) return false;
			var result = await _userManager.DeleteAsync(user);
			return result.Succeeded;
		}
		public async Task<bool> UpdateAsync(string id, IdentityUser user)
		{
			var existing_user = await _userManager.FindByIdAsync(id);
			if (existing_user is null) return false;

			existing_user.UserName = user.UserName;
			existing_user.Email = user.Email;

			var result = await _userManager.UpdateAsync(existing_user);
			return result.Succeeded;
		}

		public async Task<bool> ChangePasswordAsync(string id, string oldPassword, string newPassword)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user is null) return false;

			var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
			return result.Succeeded;
		}
	}
}
