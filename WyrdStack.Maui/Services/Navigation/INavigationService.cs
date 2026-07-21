using System;
using System.Collections.Generic;
using System.Text;
using WyrdStack.Maui.ViewModels;

namespace WyrdStack.Maui.Services.Navigation
{
	public interface INavigationService
	{
		public Task GoToAsync<TPage>() where TPage: Page;
		public Task GoToAsync<TPage>(object parameter) where TPage : Page;
		public Task GoBackAsync<TPage>() where TPage : Page;
	}
}
