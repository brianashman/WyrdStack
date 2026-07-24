using System;

namespace AvaloniaApplication.Models;

public class SideBarItem
{
    public required string Title { get; set; }
    public required string IconKey { get; set; }
    public required Type ViewModelType { get; set; }
}