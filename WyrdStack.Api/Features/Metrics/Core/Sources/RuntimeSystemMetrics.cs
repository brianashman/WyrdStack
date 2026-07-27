using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using WyrdStack.Api.Features.Metrics.Core;

namespace WyrdStack.Api.Features.Metrics.Core.Sources
{
	public class RuntimeSystemMetrics: SystemMetricsBase
	{
		public override Metadata MetricsMetadata { get; set; }
		public string FrameworkDescription { get; set; }
		public string RuntimeIdentifier { get; set; }
		public string ProcessArchitecture { get; set; }
		public string OSDescription { get; set; }
		public string OSArchitecture { get; set; }
		public bool Is64BitProcess { get; set; }
		public bool Is64BitOperatingSystem { get; set; }
		public int CpuCount { get; set; }
		public long WorkingSetBytes { get; set; }
		public TimeSpan Uptime { get; set; }
	}
}
