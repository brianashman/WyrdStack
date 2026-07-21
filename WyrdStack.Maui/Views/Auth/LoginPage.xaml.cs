using WyrdStack.Maui.ViewModels.Auth;

namespace WyrdStack.Maui.Views.Auth;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}