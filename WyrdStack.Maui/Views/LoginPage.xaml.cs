using WyrdStack.Maui.ViewModels;

namespace WyrdStack.Maui.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
	protected override void OnSizeAllocated(double width, double height)
	{
		base.OnSizeAllocated(width, height);
		if (width > 0 && height > 0)
		{
			LoginBorder.WidthRequest = width * 0.8;
			LoginBorder.HeightRequest = height * 0.6;
		}
	}
}