using Avalonia.Controls;
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

        protected override void OnDataContextChanged(System.EventArgs e)
        {
             base.OnDataContextChanged(e);
             if (DataContext is ClippyViewModel vm)
             {
                 // Handle specific VM events if needed, for now standard binding is enough
             }
        }
    }
}
