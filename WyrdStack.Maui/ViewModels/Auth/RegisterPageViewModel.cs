using System;
using System.Collections.Generic;
using System.Text;
using WyrdStack.Maui.Services.Navigation;
using WyrdStack.Maui.Views.Auth;

namespace WyrdStack.Maui.ViewModels.Auth
{
	public partial class RegisterPageViewModel : AuthCardComponentViewModel
	{
		private readonly INavigationService _navigationService;
		public RegisterPageViewModel(INavigationService _service)
		{
			_navigationService = _service;
			Title = "Register Account";
			ActionButtonText = "Create Account";
			IsPassword = true;
		}
		private bool CheckUsername(string username)
		{
			// Implementation for checking username
			if (string.IsNullOrEmpty(username))
			{
				StatusMessage = "Username is required.";
				return false;
			}

			const string allowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_";
			if (username.Any(c => !allowedCharacters.Contains(c)))
			{
				StatusMessage = "Username can only contain letters, numbers, hyphens, and underscores.";
				return false;
			}
			return true;
		}
		private bool CheckPassword(string password)
		{
			// Implementation for checking password
			if (string.IsNullOrEmpty(password))
			{
				StatusMessage = "Password is required.";
				return false;
			}

			if (password.Length < 8)
			{
				StatusMessage = "Password must be at least 8 characters long.";
				return false;
			}

			var hasUppercase = password.Any(char.IsUpper);
			if (!hasUppercase) StatusMessage = "You must have at least one uppercase letter in your password.";

			var hasLowercase = password.Any(char.IsLower);
			if (!hasLowercase) StatusMessage = "You must have at least one lowercase letter in your password.";

			var hasDigit = password.Any(char.IsDigit);
			if (!hasDigit) StatusMessage = "You must have at least one digit in your password.";

			return hasUppercase && hasLowercase && hasDigit;
		}
		protected override void ExecuteActionButton()
		{
			if (CheckUsername(Email) is false || CheckPassword(Password) is false) return;
			else StatusMessage = string.Empty;
		}
		protected override async Task NavigateToAsync()
		{
			await _navigationService.GoToAbsoluteAsync("LoginPage");
		}
	}
}
