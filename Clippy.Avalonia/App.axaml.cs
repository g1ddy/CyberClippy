using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Clippy.Core.Services;
using Clippy.Core.ViewModels;
using Clippy.Avalonia.Services.Mocks;
using System;
using SharpHook;
using SharpHook.Native;
using Avalonia.Threading;

namespace Clippy.Avalonia
{
    public partial class App : Application
    {
        public new static App? Current => (App?)Application.Current;

        public IServiceProvider Services { get; private set; } = null!;
        private SimpleGlobalHook? _globalHook;

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            Services = ConfigureServices();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
#if PHASE3_TRAY_ICON
                desktop.ShutdownMode = global::Avalonia.Controls.ShutdownMode.OnExplicitShutdown;
#endif

                desktop.MainWindow = new MainWindow
                {
                    DataContext = Services.GetRequiredService<ClippyViewModel>()
                };

                SetupGlobalHotkeys(desktop);

                desktop.Exit += (s, e) => _globalHook?.Dispose();
            }

            base.OnFrameworkInitializationCompleted();
        }

        private bool _isMetaPressed;

        private void SetupGlobalHotkeys(IClassicDesktopStyleApplicationLifetime desktop)
        {
            _globalHook = new SimpleGlobalHook();

            _globalHook.KeyPressed += (s, e) =>
            {
                if (e.Data.KeyCode == KeyCode.VcLeftMeta || e.Data.KeyCode == KeyCode.VcRightMeta)
                {
                    _isMetaPressed = true;
                }

                if (e.Data.KeyCode == KeyCode.VcC && _isMetaPressed)
                {
                    Dispatcher.UIThread.Post(() =>
                    {
                        if (desktop.MainWindow is { } mainWindow)
                        {
                            if (mainWindow.IsVisible)
                            {
                                mainWindow.Hide();
                            }
                            else
                            {
                                mainWindow.Show();
                                mainWindow.Activate();
                            }
                        }
                    });
                }
            };

            _globalHook.KeyReleased += (s, e) =>
            {
                if (e.Data.KeyCode == KeyCode.VcLeftMeta || e.Data.KeyCode == KeyCode.VcRightMeta)
                {
                    _isMetaPressed = false;
                }
            };

            _globalHook.RunAsync();
        }

        private void TrayIcon_Show_Click(object? sender, EventArgs e)
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop && desktop.MainWindow is { } mainWindow)
            {
                mainWindow.Show();
                mainWindow.Activate();
            }
        }

        private void TrayIcon_Settings_Click(object? sender, EventArgs e)
        {
            // TODO: Open Settings Window (Phase 4)
        }

        private void TrayIcon_Exit_Click(object? sender, EventArgs e)
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                _globalHook?.Dispose();
                desktop.Shutdown();
            }
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IChatService, MockChatService>();
            services.AddSingleton<IKeyService, MockKeyService>();
            services.AddSingleton<ISettingsService, MockSettingsService>();
            services.AddSingleton<ClippyViewModel>();

            return services.BuildServiceProvider();
        }
    }
}
