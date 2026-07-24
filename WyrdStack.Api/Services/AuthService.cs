using Microsoft.AspNetCore.Identity;

namespace WyrdStack.Api.Services
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<IdentityUser> _userManager;
		public AuthService(UserManager<IdentityUser> userManager)
		{
			_userManager = userManager;
		}
		public Task<bool> CreateAsync(IdentityUser user)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(string id)
		{
			throw new NotImplementedException();
		}

		public Task<List<IdentityUser>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<IdentityUser?> GetAsync(string id)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(string id, IdentityUser user)
		{
			throw new NotImplementedException();
		}
	}
}
