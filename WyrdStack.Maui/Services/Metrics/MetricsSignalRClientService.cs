using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using WyrdStack.Maui.Models.Dtos.Metrics;

namespace WyrdStack.Maui.Services.Metrics
{
	public class MetricsSignalRClientService
	{
		private readonly HubConnection _hubConnection;
		private readonly ILogger<MetricsSignalRClientService> _logger;
		public event Action<RuntimeSystemMetrics> RuntimeSystemMetricsReceived;

		public MetricsSignalRClientService(string hubUrl, Func<Task<string>> getTokenAsync, ILogger<MetricsSignalRClientService> logger)
		{
			_logger = logger;
			_hubConnection = new HubConnectionBuilder()
				.WithUrl(hubUrl,options =>
				{
					options.AccessTokenProvider = async () =>
					{
						var token = await getTokenAsync();
						return token ?? string.Empty;
					}; ;
				})
				.WithAutomaticReconnect()
				.Build();
			_hubConnection.On<RuntimeSystemMetrics>("ReceiveRuntimeMetrics", (metrics) =>
			{
				RuntimeSystemMetricsReceived?.Invoke(metrics);
				_logger.LogInformation("Received RuntimeSystemMetrics: {@Metrics}", metrics);
			});

		}
		public async Task StartAsync()
		{
			if(_hubConnection.State == HubConnectionState.Disconnected) await _hubConnection.StartAsync();
			_logger.LogInformation("Started Metrics SignalR Client");
		}
		public async Task StopAsync()
		{
			if(_hubConnection.State == HubConnectionState.Connected) await _hubConnection.StopAsync();
			_logger.LogInformation("Stopped Metrics SignalR Client");
		}
	}
}
