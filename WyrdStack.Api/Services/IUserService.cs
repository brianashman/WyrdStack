using Microsoft.AspNetCore.Identity;
using WyrdStack.Api.Models.Dtos;

namespace WyrdStack.Api.Services
{
	public interface IUserService
	{
		public Task<List<IdentityUser>> GetAllAsync();
		public Task<IdentityUser?> GetAsync(string id);
		public Task<IdentityResult> CreateAsync(IdentityUser user, string password);
		public Task<IdentityResult> UpdateAsync(string id, UpdateUserDTO dto);
		public Task<bool> DeleteAsync(string id);
		public Task<IdentityResult> ChangePasswordAsync(string id, string oldPassword, string newPassword);
	}
}