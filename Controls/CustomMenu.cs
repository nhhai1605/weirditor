using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace weirditor.Controls;

public class CustomMenu : Menu
{
    public static readonly DependencyProperty PopupPlacementProperty =
        DependencyProperty.Register(
            nameof(PopupPlacement),
            typeof(PlacementMode),
            typeof(CustomMenu),
            new PropertyMetadata(PlacementMode.Bottom)); // Default placement

    public PlacementMode PopupPlacement
    {
        get => (PlacementMode)GetValue(PopupPlacementProperty);
        set => SetValue(PopupPlacementProperty, value);
    }
}