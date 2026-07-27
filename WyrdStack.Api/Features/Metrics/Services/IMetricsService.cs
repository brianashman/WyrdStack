	using WyrdStack.Api.Features.Metrics.Providers;

	namespace WyrdStack.Api.Features.Metrics.Services
	{
		public interface IMetricsService
		{
			public Task PushRuntimeMetricsAsync();
			//public Task PushRuntimeMetricsAsync();
			//public Task PushRuntimeMetricsAsync();
		}
	}
