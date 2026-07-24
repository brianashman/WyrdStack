using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WyrdStack.Api.Controllers
{
	[Route("api/auth")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		public
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetAll(int id)
		{
			return Ok();
		}

		[HttpGet("{id}")]
		[Authorize(Roles = "User")]
		public async Task<IActionResult> GetById(int id)
		{
			return Ok();
		}
		[HttpPost("register_with_username")]
		public async Task<IActionResult> Post([FromBody] string value)
		{
			return Ok();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] string value)
		{
			return Ok();	
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			return Ok();
		}
	}
}
