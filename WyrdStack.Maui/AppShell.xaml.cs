using Microsoft.Extensions.DependencyInjection;
using WyrdStack.Maui.Views;
using WyrdStack.Maui.Views.Auth;

namespace WyrdStack.Maui
{
	public partial class AppShell : Shell
	{
		private readonly IServiceProvider _serviceProvider;

		public AppShell(IServiceProvider serviceProvider)
		{
			InitializeComponent();
			_serviceProvider = serviceProvider;
			Loaded += AppShell_Loaded!;
		}

		private async void AppShell_Loaded(object sender, EventArgs e)
		{
			try
			{
				// Check if an auth token exists in secure storage
				var token = await SecureStorage.Default.GetAsync("auth_token");

				if (string.IsNullOrEmpty(token))
				{
					// No token found, navigate to Login
					await Navigation.PushAsync(_serviceProvider.GetRequiredService<LoginPage>());
				}
				else
				{
					// Token exists, go straight to the main app route
					await Shell.Current.GoToAsync("//MainPage");
				}
			}
			catch (Exception)
			{
				// Fallback to login if secure storage fails or throws an exception
				await Navigation.PushAsync(_serviceProvider.GetRequiredService<LoginPage>());
			}
		}
	}
}