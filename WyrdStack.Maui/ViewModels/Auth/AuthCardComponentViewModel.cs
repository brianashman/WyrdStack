using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace WyrdStack.Maui.ViewModels.Auth
{
	public abstract partial class AuthCardComponentViewModel: BaseViewModel
	{
		[ObservableProperty] public partial string Title { get; set; }
		
		[ObservableProperty] public partial string Username { get; set; }

		[ObservableProperty] public partial string Password { get; set; }

		[ObservableProperty] public partial bool IsPassword { get; set; }

		[ObservableProperty] public partial string ActionButtonText { get; set; }

		[ObservableProperty] public partial string StatusMessage { get; set; }

		[RelayCommand]
		public void TogglePasswordVisibility() => IsPassword = !IsPassword;

		[RelayCommand] protected virtual void ExecuteActionButton() { }
	}
}
