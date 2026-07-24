using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WyrdStack.Api.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WyrdStack.Api.Controllers
{
	[Route("api/users")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;
		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetAllUsers(int id)
		{
			var user = await _userService.GetAsync(id.ToString());
			if (user is null) return NotFound();
			//return Ok(user);
		}

		[HttpGet("{id}")]
		[Authorize(Roles = "User")]
		public async Task<IActionResult> GetUserById(int id)
		{
			var user = await _userService.GetAsync(id.ToString()); 
			if (user is null) return NotFound();
			//return Ok(user);
		}
		[HttpPost("register_with_username")]
		public async Task<IActionResult> PostUser([FromBody] string value)
		{
			
			//return Ok();
		}

		[HttpPut("{id}")]
		[Authorize(Roles = "User")]
		public async Task<IActionResult> PutUser(int id, [FromBody] string value)
		{
			return Ok();	
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "User")]
		public async Task<IActionResult> Delete(int id)
		{
			return Ok();
		}

		[HttpPatch("change-password")]
		[Authorize(Roles = "User")]
		public async Task<IActionResult> ChangePassword(int id, string oldPassword, string newPassword)
		{
			var result = await _userService.ChangePasswordAsync(id.ToString(), oldPassword, newPassword);
			return result ? Ok() : BadRequest();
		}
	}
}
