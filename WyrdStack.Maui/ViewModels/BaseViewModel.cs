using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace WyrdStack.Maui.ViewModels
{
	public abstract partial class BaseViewModel: ObservableObject
	{
		public virtual void OnNavigatedTo(object parameter) { }
	}
}
