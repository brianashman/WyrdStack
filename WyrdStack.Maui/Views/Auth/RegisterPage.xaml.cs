using WyrdStack.Maui.ViewModels.Auth;

namespace WyrdStack.Maui.Views.Auth;

public partial class RegisterPage : ContentPage
{
	public RegisterPage(RegisterPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}