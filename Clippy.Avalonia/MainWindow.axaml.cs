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
