using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using WyrdStack.Maui.ViewModels;

namespace WyrdStack.Maui.Services.Navigation
{
	public class NavigationService : INavigationService
	{
		private readonly IServiceProvider _serviceProvider;
		public NavigationService(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public async Task GoBackAsync<TPage>() where TPage : Page
		{
			var page = _serviceProvider.GetService<TPage>();
			if (page is not null)
			{
				await Shell.Current.GoToAsync("..");
			}
			else return;
		}

		public async Task GoToAbsoluteAsync(string path)
		{
			if (string.IsNullOrEmpty(path)) return;
			path = path.Trim();
			if (path.StartsWith("//"))
				await Shell.Current.GoToAsync(path);
			else await Shell.Current.GoToAsync($"//{path}");
		}

		public async Task GoToAsync<TPage>() where TPage : Page
		{
			var page = _serviceProvider.GetService<TPage>();
			if (page is not null)
				await Shell.Current.GoToAsync(typeof(TPage).Name);
			else return;
		}

		public async Task GoToAsync<TPage>(object parameter) where TPage : Page
		{
			var page = _serviceProvider.GetService<TPage>();
			if (page is not null)
				await Shell.Current.GoToAsync(typeof(TPage).Name);
			else return;

			if (page.BindingContext is BaseViewModel viewModel)
			{
				viewModel.OnNavigatedTo(parameter);
			}
		}

		public async Task GoToModalAsync<TPage>(object parameter) where TPage : Page
		{
			var page = _serviceProvider.GetService<TPage>();
			if (page is not null)
				await Shell.Current.Navigation.PushModalAsync(_serviceProvider.GetRequiredService<TPage>());
			else return;
		}

		public async Task GoToPathAsync(string path)
		{
			if (string.IsNullOrEmpty(path)) return;
			path = path.Trim();
			await Shell.Current.GoToAsync(path);
		}

		public async Task GoToRootAsync()
		{
			await Shell.Current.GoToAsync("//");
		}
	}
}