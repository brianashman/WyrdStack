using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace WyrdStack.Maui.Models
{
    public enum TaskStatus
	{
		NotStarted,
		InProgress,
		Completed
	}
	public enum TaskPriority
	{
		Low,
		Medium,
		High
	}
	public class TaskModel
    {
		[AutoIncrement, PrimaryKey]
		public int Id { get; set; }
        public string Title { get; set; }
		public string? Description { get; set; }
		public TaskStatus Status { get; set; }
		public TaskPriority Priority { get; set; }

		public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

		public string? AssignedTo { get; set; }

	}
}
