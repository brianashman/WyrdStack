using CommunityToolkit.Mvvm.ComponentModel;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using WyrdStack.Maui.Models.Dtos;
using WyrdStack.Maui.Models.Dtos.Request;
using WyrdStack.Maui.Services.Api;
using WyrdStack.Maui.Services.Navigation;
using WyrdStack.Maui.Views.Auth;
using System.ComponentModel.DataAnnotations;

namespace WyrdStack.Maui.ViewModels.Auth
{
	public partial class RegisterPageViewModel : AuthCardComponentViewModel
	{
		private readonly INavigationService _navigationService;
		private readonly IApiClient _client;
		public RegisterPageViewModel(INavigationService _service, IApiClient _apiClient)
		{
			_navigationService = _service;
			_client = _apiClient;
			Title = "Register Account";
			ActionButtonText = "Create Account";
			IsPassword = true;
			IsLoading = false;
			HasUsernameEntry = true;
		}
		private bool CheckEmail(string email)
		{
			if (string.IsNullOrEmpty(email))
			{
				StatusMessage = "Email is required.";
				return false;
			}
			var emailAttribute = new EmailAddressAttribute();
			if (!emailAttribute.IsValid(email))
			{
				StatusMessage = "Invalid email format.";
				return false;
			}
			return true;
		}
		private bool CheckUsername(string username)
		{
			if (string.IsNullOrEmpty(username))
			{
				StatusMessage = "Username is required.";
				return false;
			}
			if (username.Length < 3)
			{
				StatusMessage = "Username must be at least 3 characters long.";
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
		protected override async void ExecuteActionButton()
		{
			if (CheckEmail(Email) is false || CheckUsername(Username) is false || CheckPassword(Password) is false) return;
			else StatusMessage = string.Empty;
			IsLoading = true;
			try
			{
				var cleanEmail = Email.Trim().ToLowerInvariant();
				var cleanUsername = Username.Trim().ToLowerInvariant();
				var cleanPassword = Password;

				var request = new CreateUserRequest { Email = cleanEmail, Username = cleanUsername, Password = cleanPassword };
				var response = await _client.CreateUserAsync(request);

				if (response is not null) StatusMessage = "User created successfully.";
				await _navigationService.GoToAbsoluteAsync("//MainPage");
			}
			catch (ApiException ex)
			{
				var errorContent = ex.Content;
				StatusMessage = $"Failed: {errorContent}";
				IsLoading = false;
			}
			catch (HttpRequestException)
			{
				StatusMessage = "Server not reachable. Please check your connection.";
				IsLoading = false;
			}
			catch (TaskCanceledException)
			{
				StatusMessage = "The request timed out. Please try again.";
				IsLoading = false;
			}
			catch (Exception ex)
			{
				StatusMessage = $"An unexpected error occurred: {ex.Message}";
				IsLoading = false;
			}
		}
		protected override async Task NavigateToAsync()
		{
			await _navigationService.GoToAbsoluteAsync("LoginPage");
		}
	}
}