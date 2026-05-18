using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Clippy.Avalonia.Controls.Settings;

public partial class SettingsDisplayControl : UserControl
{
    public static readonly StyledProperty<string> TitleProperty =
        AvaloniaProperty.Register<SettingsDisplayControl, string>(nameof(Title));

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly StyledProperty<string> DescriptionProperty =
        AvaloniaProperty.Register<SettingsDisplayControl, string>(nameof(Description));

    public string Description
    {
        get => GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public static readonly StyledProperty<object> IconProperty =
        AvaloniaProperty.Register<SettingsDisplayControl, object>(nameof(Icon));

    public object Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly StyledProperty<object> SettingsActionableElementProperty =
        AvaloniaProperty.Register<SettingsDisplayControl, object>(nameof(SettingsActionableElement));

    public object SettingsActionableElement
    {
        get => GetValue(SettingsActionableElementProperty);
        set => SetValue(SettingsActionableElementProperty, value);
    }

    public static readonly StyledProperty<object> AdditionalDescriptionContentProperty =
        AvaloniaProperty.Register<SettingsDisplayControl, object>(nameof(AdditionalDescriptionContent));

    public object AdditionalDescriptionContent
    {
        get => GetValue(AdditionalDescriptionContentProperty);
        set => SetValue(AdditionalDescriptionContentProperty, value);
    }

    public SettingsDisplayControl()
    {
        InitializeComponent();
    }
}
