using System;
using System.Collections.Generic;
using System.Text;

namespace WyrdStack.Maui.Models.Dtos.Metrics
{
	public abstract class SystemMetricsBase
	{
		public abstract Metadata MetricsMetadata { get; set; }
	}
}
