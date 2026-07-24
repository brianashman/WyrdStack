using System;
using System.Collections.Generic;
using System.Text;

namespace WyrdStack.Maui.Models.Dtos
{
	public class GetUserRequest
	{
		public string Id { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
