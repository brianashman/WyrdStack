using Microsoft.AspNetCore.Identity;
using WyrdStack.Api.Models.Dtos;
using WyrdStack.Api.Models.Dtos.Users.Request;
using WyrdStack.Api.Models.Dtos.Users.Response;

namespace WyrdStack.Api.Mappers.UserAuth
{
	public class UserMapper : IUserMapper
	{
		public CreateUserResponseDTO ToCreateResponse(string id, CreateUserDTO createUserDTO)
		{
			return new CreateUserResponseDTO
			{
				Id = id,
				Email = createUserDTO.Email,
				UserName = createUserDTO.Username
			};
		}
		
		public GetUserDTO ToGetUserDTO(IdentityUser identityUser)
		{
			return new GetUserDTO
			{
				Id = identityUser.Id,
				Email = identityUser.Email!,
				Username = identityUser.UserName!
			};
		}

		public IdentityUser ToIdentityUser(CreateUserDTO createUserDTO)
		{
			return new IdentityUser
			{
				Email = createUserDTO.Email,
				UserName = createUserDTO.Username
			};
		}

		public IdentityUser ToIdentityUser(UpdateUserDTO updateUserDTO)
		{
			return new IdentityUser
			{
				Email = updateUserDTO.Email,
				UserName = updateUserDTO.UserName
			};
		}
	}
}
