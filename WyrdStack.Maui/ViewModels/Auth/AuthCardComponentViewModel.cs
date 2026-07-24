using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace WyrdStack.Maui.ViewModels.Auth
{
	//BaseModel for the AuthCardComponent, which is used in the LoginPage and RegisterPage
	public abstract partial class AuthCardComponentViewModel: BaseViewModel
	{
		[ObservableProperty] public partial string Title { get; set; }
		
		[ObservableProperty] public partial string Email { get; set; }
		partial void OnEmailChanged(string oldValue, string newValue)
		{
			if(newValue?.Contains(" ") is true)
			{
				Email = newValue.Replace(" ", "");
			}
		}

		[ObservableProperty] public partial string Password { get; set; }
		partial void OnPasswordChanged(string oldValue, string newValue)
		{
			if(newValue?.Contains(" ") is true)
			{
				Password = newValue.Replace(" ", "");
			}
		}

		[ObservableProperty] public partial bool IsPassword { get; set; }

		[ObservableProperty] public partial string ActionButtonText { get; set; }

		[ObservableProperty] public partial string StatusMessage { get; set; }

		[RelayCommand]
		public void TogglePasswordVisibility() => IsPassword = !IsPassword;

		[RelayCommand] protected virtual void ExecuteActionButton() { }

		[RelayCommand] protected virtual async Task NavigateToAsync() { }
	}
}
