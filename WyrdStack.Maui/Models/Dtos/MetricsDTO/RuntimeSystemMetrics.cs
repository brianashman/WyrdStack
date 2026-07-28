using System;
using System.Text.Json.Serialization;

namespace WyrdStack.Maui.Models.Dtos.Metrics
{
	public class RuntimeSystemMetrics : SystemMetricsBase
	{
		[JsonPropertyName("metricsMetadata")]
		public override Metadata MetricsMetadata { get; set; }

		[JsonPropertyName("frameworkDescription")]
		public string FrameworkDescription { get; set; }

		[JsonPropertyName("runtimeIdentifier")]
		public string RuntimeIdentifier { get; set; }

		[JsonPropertyName("processArchitecture")]
		public string ProcessArchitecture { get; set; }

		[JsonPropertyName("osDescription")]
		public string OSDescription { get; set; }

		[JsonPropertyName("osArchitecture")]
		public string OSArchitecture { get; set; }

		[JsonPropertyName("is64BitProcess")]
		public bool Is64BitProcess { get; set; }

		[JsonPropertyName("is64BitOperatingSystem")]
		public bool Is64BitOperatingSystem { get; set; }

		[JsonPropertyName("cpuCount")]
		public int CpuCount { get; set; }

		[JsonPropertyName("workingSetBytes")]
		public long WorkingSetBytes { get; set; }

		[JsonPropertyName("totalMemoryBytes")]
		public long TotalMemoryBytes { get; set; }

		[JsonPropertyName("totalStorageBytes")]
		public long TotalStorageBytes { get; set; }

		[JsonPropertyName("freeStorageBytes")]
		public long FreeStorageBytes { get; set; }

		[JsonPropertyName("uptime")]
		public TimeSpan Uptime { get; set; }
	}
}