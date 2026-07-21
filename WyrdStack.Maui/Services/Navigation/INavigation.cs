using System;
using System.Collections.Generic;
using System.Text;
using WyrdStack.Maui.ViewModels;

namespace WyrdStack.Maui.Services.Navigation
{
	public interface INavigation
	{
		public Task GoToAsync(Type route);
		public Task GoToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel;

	}
}
