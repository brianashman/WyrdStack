using Refit;
using WyrdStack.Maui.Models.Dtos;

namespace WyrdStack.Maui.Services.Api
{
	public interface IApiClient
	{
		[Post("/api/auth/login")]
		Task<AuthResponse> LoginAsync([Body] LoginRequest request);

		[Get("/api/users")]
		Task<List<GetUserRequest>> GetUsersAsync();

		[Get("/api/users/{id}")]
		Task<GetUserRequest> GetUserAsync(string id);

		[Post("/api/users/register_with_username")]
		Task<bool> CreateUserAsync([Body] CreateUserRequest user);

		[Patch("/api/users/{id}")]
		Task<bool> UpdateUserAsync(string id, [Body] UpdateUserRequest user);

		[Delete("/api/users/{id}")]
		Task<bool> DeleteUserAsync(string id);

		[Patch("/api/users/change-password")]
		Task<bool> ChangePasswordAsync([Body] ChangePasswordDTO password);
	}

	public record LoginRequest(string Username, string Password);
	public record AuthResponse(string Token);
}