using System;
using System.Collections.ObjectModel;
using AvaloniaApplication.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaApplication.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] public partial bool IsPaneOpen { get; set; } = false;
    [ObservableProperty] public partial ViewModelBase CurrentPage { get; set; }
    [ObservableProperty] public partial SideBarItem? SelectedItem { get; set; }

    partial void OnSelectedItemChanged(SideBarItem? value)
    {
        if (value is not null)
        {
            CurrentPage = (ViewModelBase)Activator.CreateInstance(value.ViewModelType)!;
        }
    }

    public ObservableCollection<SideBarItem> SideBarItems { get; } =
    [
        new()
        {
            Title = "Overview",
            IconKey = "HomeRegularGeometry",
            ViewModelType =  typeof(OverviewViewModel),
        },
        new()
        {
            Title = "All Notes",
            IconKey = "NoteRegularGeometry",
            ViewModelType = typeof(AllNotesViewModel),
        }
    ];
    
    
    [RelayCommand] private void TogglePane() => IsPaneOpen = !IsPaneOpen;
}