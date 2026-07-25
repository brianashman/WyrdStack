namespace WyrdStack.Api.Models.Dtos.Users.Response
{
	public class LoginResponseDTO
	{
		public string AccessToken { get; set; }
		public string TokenType { get; set; } = "Bearer";
	}
}
