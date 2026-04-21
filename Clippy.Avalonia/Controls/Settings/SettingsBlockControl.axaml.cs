using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Clippy.Avalonia.Controls.Settings;

public partial class SettingsBlockControl : UserControl
{
    public static readonly StyledProperty<string> TitleProperty =
        AvaloniaProperty.Register<SettingsBlockControl, string>(nameof(Title));

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly StyledProperty<string> DescriptionProperty =
        AvaloniaProperty.Register<SettingsBlockControl, string>(nameof(Description));

    public string Description
    {
        get => GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public static readonly StyledProperty<object> IconProperty =
        AvaloniaProperty.Register<SettingsBlockControl, object>(nameof(Icon));

    public object Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly StyledProperty<object> SettingsActionableElementProperty =
        AvaloniaProperty.Register<SettingsBlockControl, object>(nameof(SettingsActionableElement));

    public object SettingsActionableElement
    {
        get => GetValue(SettingsActionableElementProperty);
        set => SetValue(SettingsActionableElementProperty, value);
    }

    public static readonly StyledProperty<object> AdditionalDescriptionContentProperty =
        AvaloniaProperty.Register<SettingsBlockControl, object>(nameof(AdditionalDescriptionContent));

    public object AdditionalDescriptionContent
    {
        get => GetValue(AdditionalDescriptionContentProperty);
        set => SetValue(AdditionalDescriptionContentProperty, value);
    }

    public SettingsBlockControl()
    {
        InitializeComponent();
    }
}
