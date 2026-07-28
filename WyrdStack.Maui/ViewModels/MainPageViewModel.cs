using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UraniumUI.Dialogs;
using WyrdStack.Maui.Models.Dtos.Metrics;
using WyrdStack.Maui.Services.Api;
using WyrdStack.Maui.Services.Metrics;
using WyrdStack.Maui.Services.Navigation;

namespace WyrdStack.Maui.ViewModels
{
	public partial class MainPageViewModel : BaseViewModel
	{
		private readonly INavigationService _navigationService;
		private readonly IDialogService _dialogService;
		private readonly MetricsSignalRClientService _metricsSignalRClientService;
		public MainPageViewModel(INavigationService navigationService, IDialogService dialogService,
			MetricsSignalRClientService metricsSignalRClientService)
		{
			_navigationService = navigationService;
			_dialogService = dialogService;
			_metricsSignalRClientService = metricsSignalRClientService;

			_metricsSignalRClientService.RuntimeSystemMetricsReceived += OnMetricsRecieved;
			_ = InitializeSignalRAsync();
		}

		private async Task InitializeSignalRAsync()
		{
			try
			{
				await _metricsSignalRClientService.StartAsync();
			}
			catch (Exception ex)
			{
				// Log or handle connection errors here
				System.Diagnostics.Debug.WriteLine($"Failed to connect SignalR: {ex.Message}");
			}
		}

		[ObservableProperty] public partial int CpuCoreCount { get; set; }
		[ObservableProperty] public partial int Uptime { get; set; }
		[ObservableProperty] public partial double TotalMemoryBytes { get; set; }
		[ObservableProperty] public partial double TotalStorageBytes { get; set; }

		private void OnMetricsRecieved(RuntimeSystemMetrics metrics)
		{
			CpuCoreCount = metrics.CpuCount;
			Uptime = (int)metrics.Uptime.TotalSeconds;
			TotalMemoryBytes = Math.Round(metrics.TotalMemoryBytes / (1024.0 * 1024.0 * 1024.0), 2);
			TotalStorageBytes = Math.Round(metrics.TotalStorageBytes / (1024.0 * 1024.0 * 1024.0), 2);
		}


		[RelayCommand]
		private async void Logout()
		{
			var result = await _dialogService.ConfirmAsync(
				title: "Are you sure you want to logout?",
				message: "This will clear your session and you will need to log in again.",
				okText: "Logout",
				cancelText: "Cancel"
			);
			if (result)
			{
				SecureStorage.Default.Remove("auth_token");
				await _navigationService.GoToAbsoluteAsync("LoginPage");
			}

		}

	}
}