using WyrdStack.Maui.ViewModels;

namespace WyrdStack.Maui.Views
{
	public partial class MainPage : ContentPage
	{

		public MainPage(MainPageViewModel viewModel)
		{
			InitializeComponent();
			BindingContext = viewModel;
		}
	}
}
