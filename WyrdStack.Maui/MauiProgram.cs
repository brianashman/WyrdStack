using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using Refit;
using UraniumUI;
using WyrdStack.Maui.Services.Api;
using WyrdStack.Maui.Services.Navigation;
using WyrdStack.Maui.ViewModels;
using WyrdStack.Maui.ViewModels.Auth;
using WyrdStack.Maui.Views;
using WyrdStack.Maui.Views.Auth;

namespace WyrdStack.Maui
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.UseMauiCommunityToolkit()
				.UseUraniumUI()
				.UseUraniumUIMaterial()
				.UseMauiCommunityToolkitCore()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
					fonts.AddFontAwesomeIconFonts();
					fonts.AddMaterialIconFonts();
					fonts.AddFluentIconFonts();
				});
#if DEBUG
            builder.Logging.AddDebug();
#endif
			#region DI Injection

			// Views
			builder.Services.AddTransient<MainPage>();
			builder.Services.AddTransient<LoginPage>();
			builder.Services.AddTransient<RegisterPage>();
			builder.Services.AddTransient<AppShell>();

			// ViewModels
			builder.Services.AddTransient<MainPageViewModel>();
			builder.Services.AddTransient<LoginPageViewModel>();
			builder.Services.AddTransient<RegisterPageViewModel>();

			// Services
			builder.Services.AddSingleton<INavigationService, NavigationService>();

			// HttpClient
			builder.Services.AddRefitClient<IApiClient>()
				.ConfigureHttpClient(client =>
				{

					client.BaseAddress = new Uri("http://192.168.86.89:5237");
					client.Timeout = TimeSpan.FromSeconds(5); 
				}
			);

			#endregion

			return builder.Build();
		}
	}
}