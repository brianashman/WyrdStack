using Microsoft.AspNetCore.Identity;

namespace WyrdStack.Api.Services
{
	public interface IAuthService
	{
		public Task<List<IdentityUser>> GetAllAsync();
		public Task<IdentityUser?> GetAsync(string id);
		public Task<bool> CreateAsync(IdentityUser user);
		public Task<bool> UpdateAsync(string id, IdentityUser user);
		public Task<bool> DeleteAsync(string id);

	}
}
