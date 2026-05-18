using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Diagnostics;
using System;
using Clippy.Avalonia.ViewModels;
using Clippy.Core.Services;

namespace Clippy.Avalonia;

public partial class SettingsWindow : Window
{
    public SettingsWindow()
    {
        InitializeComponent();
    }

    public SettingsWindow(ISettingsService settingsService)
    {
        InitializeComponent();
        DataContext = new SettingsViewModel(settingsService);
    }

    private void GitHub_Click(object sender, RoutedEventArgs e)
    {
        OpenUrl("https://github.com/FireCubeStudios/Clippy");
    }

    private void Discord_Click(object sender, RoutedEventArgs e)
    {
        OpenUrl("https://discord.gg/3WYcKat");
    }

    private void Twitter_Click(object sender, RoutedEventArgs e)
    {
        OpenUrl("https://twitter.com/FireCubeStudios");
    }

    private void Bluesky_Click(object sender, RoutedEventArgs e)
    {
        OpenUrl("https://bsky.app/profile/firecube.bsky.social");
    }

    private void Exit_Click(object sender, RoutedEventArgs e)
    {
        if (global::Avalonia.Application.Current?.ApplicationLifetime is global::Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Shutdown();
        }
    }

    private void OpenUrl(string url)
    {
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unable to open URL: {ex.Message}");
        }
    }
}
