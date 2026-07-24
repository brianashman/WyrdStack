using System;
using System.Collections.Generic;
using System.Text;

namespace WyrdStack.Maui.Models.Dtos.Request
{
	public class UpdateUserRequest
	{
		public string? Email { get; set; }  
		public string? Password { get; set; }
	}
}
