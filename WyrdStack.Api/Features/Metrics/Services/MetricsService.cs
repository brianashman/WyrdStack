using Microsoft.AspNetCore.SignalR;
using WyrdStack.Api.Features.Metrics.Core.Sources;
using WyrdStack.Api.Features.Metrics.Providers;
using WyrdStack.Api.Hubs;

namespace WyrdStack.Api.Features.Metrics.Services
{
	public class MetricsService: IMetricsService
	{
		private readonly IHubContext<MetricsHub> _hubContext;
		private readonly IMetricsProvider<RuntimeSystemMetrics> _runtimeProvider;

		public MetricsService(IHubContext<MetricsHub> hubContext, IMetricsProvider<RuntimeSystemMetrics> runtimeProvider)
		{
			_hubContext = hubContext;
			_runtimeProvider = runtimeProvider;
		}

		public async Task PushRuntimeMetricsAsync()
		{
			var metrics = await _runtimeProvider.Create();
			await _hubContext.Clients.All.SendAsync("ReceiveRuntimeMetrics", metrics);
		}
	}
}
