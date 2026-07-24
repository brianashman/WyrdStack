using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WyrdStack.Maui.Services.Api;
using WyrdStack.Maui.Services.Navigation;
using WyrdStack.Maui.Views.Auth;

namespace WyrdStack.Maui.ViewModels.Auth
{
	public partial class LoginPageViewModel : AuthCardComponentViewModel
	{
		private readonly INavigationService _navigationService;
		private readonly IApiClient _apiClient;

		// Observable property to control hiding the card / showing the loading indicator
		[ObservableProperty]
		private bool isLoading;

		public LoginPageViewModel(INavigationService service, IApiClient apiClient)
		{
			_navigationService = service;
			_apiClient = apiClient;
			Title = "Login";
			ActionButtonText = "Sign In";
			IsPassword = true;
		}

		private bool CheckUsername(string username)
		{
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

			if (!password.Any(char.IsUpper))
			{
				StatusMessage = "You must have at least one uppercase letter in your password.";
				return false;
			}

			if (!password.Any(char.IsLower))
			{
				StatusMessage = "You must have at least one lowercase letter in your password.";
				return false;
			}

			if (!password.Any(char.IsDigit))
			{
				StatusMessage = "You must have at least one digit in your password.";
				return false;
			}

			return true;
		}

		protected override async void ExecuteActionButton()
		{
			if (CheckUsername(Email) is false || CheckPassword(Password) is false) return;

			StatusMessage = string.Empty;
			IsLoading = true; // Show loading screen / hide card

			try
			{
				var response = await _apiClient.LoginAsync(new IdentityLoginRequest(Email, Password));

				if (!string.IsNullOrEmpty(response?.AccessToken))
				{
					StatusMessage = "Success!";
					await _navigationService.GoToAbsoluteAsync("//MainPage");
				}
				else
				{
					StatusMessage = "Server returned an empty response.";
					IsLoading = false; // Restore card if it fails
				}
			}
			catch (ApiException ex)
			{
				StatusMessage = ex.StatusCode == System.Net.HttpStatusCode.Unauthorized
					? "Invalid email or password."
					: $"API Error: {ex.StatusCode}";
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
			await _navigationService.GoToAbsoluteAsync("RegisterPage");
		}
	}
}