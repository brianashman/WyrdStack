namespace WyrdStack.Api.Services.Metrics.Core
{
	public abstract class SystemMetricsBase<TMetrics>
	{
		public Metadata Metadata { get; set; }
		public TMetrics Metrics { get; set; }
	}
}
