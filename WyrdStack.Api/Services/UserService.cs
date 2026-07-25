using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WyrdStack.Api.Models.Dtos;
using WyrdStack.Api.Models.Dtos.Users.Request;
using WyrdStack.Api.Models.Dtos.Users.Response;

namespace WyrdStack.Api.Services
{
	public class UserService : IUserService
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IConfiguration _configuration;

		public UserService(
			UserManager<IdentityUser> userManager,
			RoleManager<IdentityRole> roleManager,
			IConfiguration configuration)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_configuration = configuration;
		}

		public async Task<List<IdentityUser>> GetAllAsync() => await _userManager.Users.ToListAsync();

		public async Task<IdentityUser?> GetAsync(string id) => await _userManager.FindByIdAsync(id);

		public async Task<IdentityResult> CreateAsync(IdentityUser user, string password)
		{
			var result = await _userManager.CreateAsync(user, password);
			if (result.Succeeded)
			{
				string defaultRole = "User";
				if (!await _roleManager.RoleExistsAsync(defaultRole))
				{
					await _roleManager.CreateAsync(new IdentityRole(defaultRole));
				}

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

		public async Task<LoginResponseDTO?> LoginAsync(LoginDTO dto)
		{
			var normalizedEmail = dto.Email?.Trim().ToLowerInvariant();
			var user = await _userManager.FindByEmailAsync(normalizedEmail);

			if (user == null) return null;

			var isPasswordValid = await _userManager.CheckPasswordAsync(user, dto.Password);
			if (!isPasswordValid) return null;

			var token = await GenerateJwtTokenAsync(user);

			return new LoginResponseDTO
			{
				AccessToken = token,
				TokenType = "Bearer"
			};
		}

		private async Task<string> GenerateJwtTokenAsync(IdentityUser user)
		{
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Id),
				new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
				new Claim(ClaimTypes.NameIdentifier, user.Id),
				new Claim(ClaimTypes.Name, user.UserName ?? string.Empty)
			};

			var roles = await _userManager.GetRolesAsync(user);
			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "YourSuperSecretKeyThatIsLongEnough123!"));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddDays(7),
				Issuer = _configuration["Jwt:Issuer"],
				Audience = _configuration["Jwt:Audience"],
				SigningCredentials = creds
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var createdToken = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(createdToken);
		}
	}
}