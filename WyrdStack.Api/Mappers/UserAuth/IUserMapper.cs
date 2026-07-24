using Microsoft.AspNetCore.Identity;
using WyrdStack.Api.Models.Dtos.Users.Request;
using WyrdStack.Api.Models.Dtos.Users.Response;

namespace WyrdStack.Api.Mappers.UserAuth
{
	public interface IUserMapper
	{
		public IdentityUser ToIdentityUser(CreateUserDTO createUserDTO);
		public IdentityUser ToIdentityUser(UpdateUserDTO updateUserDTO);

		public GetUserDTO ToGetUserDTO(IdentityUser identityUser);

		public CreateUserResponseDTO ToCreateResponse(string id, CreateUserDTO createUserDTO);
	}
}
