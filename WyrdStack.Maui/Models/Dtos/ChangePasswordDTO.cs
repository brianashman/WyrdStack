using System;
using System.Collections.Generic;
using System.Text;

namespace WyrdStack.Maui.Models.Dtos
{
	public class ChangePasswordDTO
	{
		public string? OldPassword { get; set; }
		public string NewPassword { get; set; }
	}
}
