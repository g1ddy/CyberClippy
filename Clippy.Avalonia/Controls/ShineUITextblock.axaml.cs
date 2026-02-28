using Avalonia;
using Avalonia.Controls;

namespace Clippy.Avalonia.Controls
{
    public partial class ShineUITextblock : UserControl
    {
        public static readonly StyledProperty<string> TextProperty =
            AvaloniaProperty.Register<ShineUITextblock, string>(nameof(Text));

        public static readonly StyledProperty<bool> IsLoadingProperty =
            AvaloniaProperty.Register<ShineUITextblock, bool>(nameof(IsLoading));

        public string Text
        {
            get => GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public bool IsLoading
        {
            get => GetValue(IsLoadingProperty);
            set => SetValue(IsLoadingProperty, value);
        }

        public bool IsTextVisible => !IsLoading && !string.IsNullOrEmpty(Text);

        public ShineUITextblock()
        {
            InitializeComponent();
        }

    }
}