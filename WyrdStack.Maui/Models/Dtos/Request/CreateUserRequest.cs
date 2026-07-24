
using System;
using System.Collections.Generic;
using System.Text;

namespace WyrdStack.Maui.Models.Dtos.Request
{
	public class CreateUserRequest
	{
		public string Email { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
	}
}
