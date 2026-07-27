namespace WyrdStack.Api.Services.Metrics.Core
{
	public interface ISystemMetricsSource
	{
		public Task<object> GetMetricsAsync(CancellationToken cancellationToken);
	}
}
