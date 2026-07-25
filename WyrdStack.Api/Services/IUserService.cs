using Microsoft.AspNetCore.Identity;
using WyrdStack.Api.Models.Dtos.Users.Request;
using WyrdStack.Api.Models.Dtos.Users.Response;

namespace WyrdStack.Api.Services
{
	public interface IUserService
	{
		Task<List<IdentityUser>> GetAllAsync();
		Task<IdentityUser?> GetAsync(string id);
		Task<IdentityResult> CreateAsync(IdentityUser user, string password);
		Task<bool> DeleteAsync(string id);
		Task<IdentityResult> UpdateAsync(string id, UpdateUserDTO dto);
		Task<IdentityResult> ChangePasswordAsync(string id, string oldPassword, string newPassword);

		Task<LoginResponseDTO?> LoginAsync(LoginDTO dto);
	}
}