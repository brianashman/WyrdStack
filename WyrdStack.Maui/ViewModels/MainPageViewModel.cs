using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
using WyrdStack.Maui.Services.Api;
using WyrdStack.Maui.Services.Navigation;

namespace WyrdStack.Maui.ViewModels
{
	public partial class MainPageViewModel : BaseViewModel
	{
		private readonly INavigationService _navigationService;
		public MainPageViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
		}

		[RelayCommand]
		private void Logout()
		{
			SecureStorage.Default.Remove("auth_token");
			_navigationService.GoToAbsoluteAsync("LoginPage");
		}
	}
}
