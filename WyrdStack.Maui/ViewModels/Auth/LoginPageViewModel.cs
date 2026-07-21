using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace WyrdStack.Maui.ViewModels.Auth
{
	public partial class LoginPageViewModel: AuthCardComponentViewModel
	{
		public LoginPageViewModel() {
			Title = "Login";
			ActionButtonText = "Sign In";
			IsPassword = true;
		}


		//[ObservableProperty]
		//private string loginStatusMessage = string.Empty;



		//[ObservableProperty]
		//private string username = string.Empty;
		//partial void OnUsernameChanged(string? oldValue, string newValue)
		//{
		//	Username = newValue.Trim();
		//}

		//[ObservableProperty]
		//private bool isPassword = true;
		//[ObservableProperty]
		//private string password = string.Empty;
		//partial void OnPasswordChanged(string value)
		//{
		//	Password = value.Trim();
		//}

		//[RelayCommand] private void TogglePasswordVisibility() => IsPassword = !IsPassword;
		
		//private bool CheckUsername(string username)
		//{
		//	// Implementation for checking username
		//	if(string.IsNullOrEmpty(username))
		//	{
		//		LoginStatusMessage = "Username is required.";
		//		return false;
		//	}

		//	const string allowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_";
		//	if(username.Any(c => !allowedCharacters.Contains(c)))
		//	{
		//		LoginStatusMessage = "Username can only contain letters, numbers, hyphens, and underscores.";
		//		return false;
		//	}
		//	return true;
		//}
		//private bool CheckPassword(string password)
		//{
		//	// Implementation for checking password
		//	if(string.IsNullOrEmpty(password))
		//	{
		//		LoginStatusMessage = "Password is required.";
		//		return false;
		//	}

		//	if(password.Length < 8)
		//	{
		//		LoginStatusMessage = "Password must be at least 8 characters long.";
		//		return false;
		//	}

		//	var hasUppercase = password.Any(char.IsUpper);
		//	if(!hasUppercase) LoginStatusMessage = "You must have at least one uppercase letter in your password.";

		//	var hasLowercase = password.Any(char.IsLower);
		//	if (!hasLowercase) LoginStatusMessage = "You must have at least one lowercase letter in your password.";

		//	var hasDigit = password.Any(char.IsDigit);
		//	if(!hasDigit) LoginStatusMessage = "You must have at least one digit in your password.";

		//	return hasUppercase && hasLowercase && hasDigit;
		//}

		//[RelayCommand] private void Login()
		//{
		//	bool checks_passed = false;
		//	if (CheckUsername(Username) is false || CheckPassword(Password) is false)
		//	{
		//		checks_passed = false;
		//	}
		//	else
		//	{
		//		checks_passed = true;
		//		LoginStatusMessage = string.Empty;
		//	}
		//}
	}
}
