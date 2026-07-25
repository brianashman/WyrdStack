using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UraniumUI.Dialogs;
using WyrdStack.Maui.Services.Api;
using WyrdStack.Maui.Services.Navigation;

namespace WyrdStack.Maui.ViewModels
{
	public partial class MainPageViewModel : BaseViewModel
	{
		private readonly INavigationService _navigationService;
		private readonly IDialogService _dialogService;
		public MainPageViewModel(INavigationService navigationService, IDialogService dialogService)
		{
			_navigationService = navigationService;
			_dialogService = dialogService;
		}

		[RelayCommand]
		private async void Logout()
		{
			var result = await _dialogService.ConfirmAsync(
				title: "Are you sure you want to logout?",
				message: "This will clear your session and you will need to log in again.",
				okText: "Logout",
				cancelText: "Cancel"
			);
			if(result)
			{
				SecureStorage.Default.Remove("auth_token");
				await _navigationService.GoToAbsoluteAsync("LoginPage");
			}	

		}

	}
}
