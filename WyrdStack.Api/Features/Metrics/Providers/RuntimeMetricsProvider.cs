using System.Diagnostics;
using System.Runtime.InteropServices;
using WyrdStack.Api.Features.Metrics.Core;
using WyrdStack.Api.Features.Metrics.Core.Sources;

namespace WyrdStack.Api.Features.Metrics.Providers
{
	public class RuntimeMetricsProvider : IMetricsProvider<RuntimeSystemMetrics>
	{
		public async Task<RuntimeSystemMetrics> Create()
		{
			var process = Process.GetCurrentProcess();
			var gcMemoryInfo = GC.GetGCMemoryInfo();

			// Get primary drive info (e.g., C:\ on Windows or / on Linux)
			var rootDrive = new DriveInfo(Path.GetPathRoot(AppContext.BaseDirectory) ?? "/");

			return new RuntimeSystemMetrics
			{
				MetricsMetadata = new Metadata
				{
					SourceName = "RuntimeMetricsProvider"
				},
				FrameworkDescription = RuntimeInformation.FrameworkDescription,
				RuntimeIdentifier = RuntimeInformation.RuntimeIdentifier,
				ProcessArchitecture = RuntimeInformation.ProcessArchitecture.ToString(),
				OSDescription = RuntimeInformation.OSDescription,
				OSArchitecture = RuntimeInformation.OSArchitecture.ToString(),
				Is64BitProcess = Environment.Is64BitProcess,
				Is64BitOperatingSystem = Environment.Is64BitOperatingSystem,
				CpuCount = Environment.ProcessorCount,

				// --- Memory Details ---
				WorkingSetBytes = process.WorkingSet64,                          
				TotalMemoryBytes = gcMemoryInfo.TotalAvailableMemoryBytes,       

				// --- Storage Details (Primary Drive) ---
				TotalStorageBytes = rootDrive.TotalSize,                         
				FreeStorageBytes = rootDrive.AvailableFreeSpace,                 

				Uptime = DateTime.UtcNow - process.StartTime.ToUniversalTime()
			};
		}
	}
}