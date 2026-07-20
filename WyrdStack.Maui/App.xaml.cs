using Microsoft.Extensions.DependencyInjection;
using WyrdStack.Maui.Views;

namespace WyrdStack.Maui
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
		}
		protected override Window CreateWindow(IActivationState? activationState)
		{
			var token = SecureStorage.Default.GetAsync("auth_token").Result;
			if(token == null)
			{
				return new Window(new LoginPage());
			}
			else
			{
				return new Window(new AppShell());
			}
		}
	}
}