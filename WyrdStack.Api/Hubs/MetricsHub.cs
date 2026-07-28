using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using WyrdStack.Api.Features.Metrics.Core.Sources;
using WyrdStack.Api.Features.Metrics.Providers;

namespace WyrdStack.Api.Hubs
{
	[Authorize(Policy = "SignalRPolicy")]
	public class MetricsHub : Hub
	{
		public async Task SendMetricsToClients(RuntimeSystemMetrics metrics)
		{
			await Clients.All.SendAsync("ReceiveRuntimeMetrics", metrics);
		}
	}
}