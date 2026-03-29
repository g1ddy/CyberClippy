using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Clippy.Core.ViewModels;

namespace Clippy.Avalonia
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if PHASE3_TRAY_ICON
            this.Closing += Window_Closing;
#endif
        }

#if PHASE3_TRAY_ICON
        private void Window_Closing(object? sender, global::Avalonia.Controls.WindowClosingEventArgs e)
        {
            // Intercept window close to keep application running for Phase 3 (Tray icon/Hotkeys)
            e.Cancel = true;
            this.Hide();
        }
#endif

        private void Hide_Click(object? sender, global::Avalonia.Interactivity.RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Settings_Click(object? sender, global::Avalonia.Interactivity.RoutedEventArgs e)
        {
            // Placeholder for Settings
        }

        private void Exit_Click(object? sender, global::Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (global::Avalonia.Application.Current?.ApplicationLifetime is global::Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.Shutdown();
            }
        }

        private void InputTextBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (e.KeyModifiers.HasFlag(KeyModifiers.Shift))
                {
                    // Allow Shift+Enter to fall through and insert a newline
                    return;
                }

                // Normal Enter - trigger the command and prevent newline
                e.Handled = true;
                if (DataContext is ClippyViewModel vm)
                {
                    if (vm.SendPromptCommand.CanExecute(null))
                    {
                        vm.SendPromptCommand.Execute(null);
                    }
                }
            }
        }
    }
}
