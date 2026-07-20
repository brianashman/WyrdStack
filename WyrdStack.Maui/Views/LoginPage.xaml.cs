using WyrdStack.Maui.ViewModels;

namespace WyrdStack.Maui.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}