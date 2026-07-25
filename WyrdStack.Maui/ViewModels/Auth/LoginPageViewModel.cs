using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

		public LoginPageViewModel(INavigationService service, IApiClient apiClient)
		{
			_navigationService = service;
			_apiClient = apiClient;
			Title = "Login";
			ActionButtonText = "Sign In";
			IsPassword = true;
			IsLoading = false;
			HasUsernameEntry = false;
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

			private bool CheckPassword(string password)
			{
				if (string.IsNullOrEmpty(password))
				{
					StatusMessage = "Password is required.";
					return false;
				}

				return true;
			}

		protected override async void ExecuteActionButton()
		{
			if (CheckEmail(Email) is false || CheckPassword(Password) is false) return;

			StatusMessage = string.Empty;
			IsLoading = true;

			try
			{
				var lowerEmail = Email.Trim().ToLowerInvariant();
				var lowerPassword = Password;

				var response = await _apiClient.LoginAsync(new IdentityLoginRequest(lowerEmail, lowerPassword));

				if (!string.IsNullOrEmpty(response?.AccessToken))
				{
					await SecureStorage.Default.SetAsync("auth_token", response.AccessToken);
					StatusMessage = "Success!";
					await _navigationService.GoToAbsoluteAsync("//MainPage");
				}
				else
				{
					StatusMessage = "Server returned an empty response.";
					IsLoading = false;
				}
			}
			catch (ApiException ex)
			{
				if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
				{
					SecureStorage.Default.Remove("auth_token");
					StatusMessage = "Invalid email or password.";
				}
				else
				{
					StatusMessage = $"API Error: {ex.StatusCode}";
				}
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