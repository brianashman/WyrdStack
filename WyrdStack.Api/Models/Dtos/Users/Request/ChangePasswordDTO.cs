namespace WyrdStack.Api.Models.Dtos.Users.Request
{
	public class ChangePasswordDTO
	{
		public string OldPassword { get; set; }
		public string NewPassword { get; set; }
	}
}
