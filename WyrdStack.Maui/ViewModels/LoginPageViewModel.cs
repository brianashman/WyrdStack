using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace WyrdStack.Maui.ViewModels
{
	public partial class LoginPageViewModel: BaseViewModel
	{
		[ObservableProperty]
		private bool isPassword = true;
		[ObservableProperty]
		private string password = string.Empty;
		partial void OnPasswordChanged(string value)
		{
			Password = value.Trim();
		}

		[RelayCommand]
		private void TogglePasswordVisibility()
		{
			IsPassword = !IsPassword;
		}
	}
}
