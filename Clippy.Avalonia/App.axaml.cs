using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Clippy.Core.Services;
using Clippy.Core.ViewModels;
using Clippy.Avalonia.Services.Mocks;
using System;

namespace Clippy.Avalonia
{
    public partial class App : Application
    {
        public new static App? Current => (App?)Application.Current;

        public IServiceProvider Services { get; private set; } = null!;

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            Services = ConfigureServices();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.ShutdownMode = global::Avalonia.Controls.ShutdownMode.OnExplicitShutdown;

                desktop.MainWindow = new MainWindow
                {
                    DataContext = Services.GetRequiredService<ClippyViewModel>()
                };
            }

            base.OnFrameworkInitializationCompleted();
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
