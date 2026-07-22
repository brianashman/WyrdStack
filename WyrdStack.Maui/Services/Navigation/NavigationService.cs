using System;
using System.Collections.Generic;
using System.Text;
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
				await Shell.Current.Navigation.PopAsync();
			}
			else return;
		}

		public async Task GoToAsync<TPage>() where TPage: Page
		{
			
			var page =_serviceProvider.GetService<TPage>();
			if (page is not null)
				await Shell.Current.Navigation.PushAsync(page);
			else return;
		}
		public async Task GoToAsync<TPage>(object parameter) where TPage : Page
		{
			var page = _serviceProvider.GetService<TPage>();
			if (page is not null)
				await Shell.Current.Navigation.PushAsync(page);
			else return;

			if (page.BindingContext is BaseViewModel viewModel)
			{
				viewModel.OnNavigatedTo(parameter);
			}
		}

	}
}
