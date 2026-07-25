using Refit;
using WyrdStack.Maui.Models.Dtos.Reponse;
using WyrdStack.Maui.Models.Dtos.Request;

namespace WyrdStack.Maui.Services.Api
{
	public interface IApiClient
	{
		[Post("/api/users/login")]
		Task<IdentityTokenResponse> LoginAsync([Body] IdentityLoginRequest request);

		[Get("/api/users")]
		Task<List<GetUserRequest>> GetUsersAsync();

		[Get("/api/users/{id}")]
		Task<GetUserRequest> GetUserAsync(string id);

		[Post("/api/users/register_with_username")]
		Task<CreateUserResponse> CreateUserAsync([Body] CreateUserRequest user);

		[Patch("/api/users/{id}")]
		Task<bool> UpdateUserAsync(string id, [Body] UpdateUserRequest user);

		[Delete("/api/users/{id}")]
		Task<bool> DeleteUserAsync(string id);

		[Patch("/api/users/change-password")]
		Task<bool> ChangePasswordAsync([Body] ChangePasswordDTO password);
	}

	public record IdentityLoginRequest(string Email, string Password);

	public class IdentityTokenResponse
	{
		public string TokenType { get; set; } = string.Empty;
		public string AccessToken { get; set; } = string.Empty;
		public int ExpiresIn { get; set; }
		public string RefreshToken { get; set; } = string.Empty;
	}
}