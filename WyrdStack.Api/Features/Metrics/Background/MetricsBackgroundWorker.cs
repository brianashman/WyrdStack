using WyrdStack.Api.Features.Metrics.Services;

namespace WyrdStack.Api.Features.Metrics.Background
{
	public class MetricsBackgroundWorker : BackgroundService
	{
		private readonly ILogger<MetricsBackgroundWorker> _logger;
		private readonly IServiceProvider _serviceProvider;
		public MetricsBackgroundWorker(ILogger<MetricsBackgroundWorker> logger, IServiceProvider serviceProvider)
		{
			_logger = logger;
			_serviceProvider = serviceProvider;
		}
		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
			while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
			{
				try
				{
					using (var scope = _serviceProvider.CreateScope())
					{
						var metricsService = scope.ServiceProvider.GetRequiredService<IMetricsService>();
						await metricsService.PushRuntimeMetricsAsync();
					}
					_logger.LogInformation("Metrics updated.");
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "An error occurred while updating metrics.");
				}
			}
		}
	}
}