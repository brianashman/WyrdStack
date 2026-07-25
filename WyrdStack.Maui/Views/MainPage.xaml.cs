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

		private async void ToolbarItem_Clicked(object sender, EventArgs e)
		{
			var confirm = await DisplayAlertAsync("Logout", "Are you sure you want to logout?", "Yes", "No");
			if (confirm)
			{
				var viewModel = BindingContext as MainPageViewModel;
				viewModel?.LogoutCommand.Execute(null);
			}
			else
			{
				return;
			}
		}
	}
}
