namespace WyrdStack.Api.Features.Metrics.Core
{
	public class Metadata
	{
		public string SourceName { get; set; }
		public DateTime Timestamp { get; set; } = DateTime.UtcNow;
	}
}
