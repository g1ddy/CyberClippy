using System;
using Clippy.Core.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;

namespace Clippy.Avalonia.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    public ISettingsService Settings { get; }

    public SettingsViewModel(ISettingsService settingsService)
    {
        Settings = settingsService;
    }
}
