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
			// Navigate to the LoginPage when the app starts
			await Shell.Current.Navigation.PushAsync(_serviceProvider.GetRequiredService<LoginPage>());
		}
	}
}
