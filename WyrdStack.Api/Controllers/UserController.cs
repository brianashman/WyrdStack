using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WyrdStack.Api.Mappers.UserAuth;
using WyrdStack.Api.Models.Dtos;
using WyrdStack.Api.Models.Dtos.Users.Request;
using WyrdStack.Api.Services;

namespace WyrdStack.Api.Controllers
{
	[Route("api/users")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly IUserMapper _userMapper;

		public UserController(IUserService userService, IUserMapper userMapper)
		{
			_userService = userService;
			_userMapper = userMapper;
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetAllUsers()
		{
			var users = await _userService.GetAllAsync();
			if (users is null) return NotFound();

			var userDtos = users.Select(_userMapper.ToGetUserDTO).ToList();

			return Ok(userDtos);
		}

		[HttpGet("{id}")]
		[Authorize(Roles = "User,Admin")]
		public async Task<IActionResult> GetUserById(string id)
		{
			if (!IsOwnerOrAdmin(id)) return Forbid();

			var user = await _userService.GetAsync(id);
			if (user is null) return NotFound();

			var getUserDTO = _userMapper.ToGetUserDTO(user);
			if (getUserDTO.Id is null || getUserDTO.Email is null || getUserDTO.Username is null)
				return BadRequest();

			return Ok(getUserDTO);
		}

		[HttpPost("register_with_username")]
		public async Task<IActionResult> PostUser(CreateUserDTO value)
		{
			var mapIdentity = _userMapper.ToIdentityUser(value);
			if (mapIdentity is null) return BadRequest();

			var result = await _userService.CreateAsync(mapIdentity, value.Password);
			if (!result.Succeeded) return BadRequest(result.Errors);

			var mapResponse = _userMapper.ToCreateResponse(mapIdentity.Id, value);
			return Ok(mapResponse);
		}

		[HttpPatch("{id}")]
		[Authorize(Roles = "User,Admin")]
		public async Task<IActionResult> PatchUser(string id, UpdateUserDTO value)
		{
			if (!IsOwnerOrAdmin(id)) return Forbid();

			var result = await _userService.UpdateAsync(id, value);
			if (!result.Succeeded) return BadRequest(result.Errors);

			return Ok();
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "User,Admin")]
		public async Task<IActionResult> DeleteUser(string id)
		{
			if (!IsOwnerOrAdmin(id)) return Forbid();

			var result = await _userService.DeleteAsync(id);
			if (!result) return BadRequest();

			return Ok();
		}

		[HttpPatch("change-password")]
		[Authorize(Roles = "User, Admin")]
		public async Task<IActionResult> ChangePassword(ChangePasswordDTO value)
		{
			var userId = User.FindFirst(ClaimTypes.NameIdentifier);
			if (userId is null) return Unauthorized();

			var result = await _userService.ChangePasswordAsync(userId.Value, value.OldPassword, value.NewPassword);
			return result.Succeeded ? Ok() : BadRequest(result.Errors);
		}

		private bool IsOwnerOrAdmin(string resourceUserId)
		{
			var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			return User.IsInRole("Admin") || currentUserId == resourceUserId;
		}
	}
}