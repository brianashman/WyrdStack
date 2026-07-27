namespace WyrdStack.Api.Features.Metrics.Providers
{
	public interface IMetricsProvider<TMetricsSource> where TMetricsSource: class
	{
		public Task<TMetricsSource> Create();
	}
}
