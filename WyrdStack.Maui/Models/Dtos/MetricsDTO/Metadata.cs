using System;
using System.Collections.Generic;
using System.Text;

namespace WyrdStack.Maui.Models.Dtos.Metrics
{
	public class Metadata
	{
		public string SourceName { get; set; }
		public DateTime Timestamp { get; set; } = DateTime.UtcNow;
	}
}
