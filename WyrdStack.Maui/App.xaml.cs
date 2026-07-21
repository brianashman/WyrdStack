using Microsoft.Extensions.DependencyInjection;
using WyrdStack.Maui.Views;
using WyrdStack.Maui.Views.Auth;

namespace WyrdStack.Maui
{
	public partial class App : Application
	{
		private readonly IServiceProvider _serviceProvider;
		public App(IServiceProvider serviceProvider)
		{
			InitializeComponent();
			_serviceProvider = serviceProvider;
		}
		protected override Window CreateWindow(IActivationState? activationState)
		{
			var token = SecureStorage.Default.GetAsync("auth_token").Result;
			if(token == null)
			{
				return new Window(new NavigationPage(_serviceProvider.GetRequiredService<LoginPage>()));
			}
			else
			{
				return new Window(_serviceProvider.GetRequiredService<AppShell>());
			}
		}
	}
}