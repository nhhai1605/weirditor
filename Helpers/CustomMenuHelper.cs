using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace weirditor.Helpers;

public static class CustomMenuHelper
{
    public static readonly DependencyProperty PopupPlacementProperty =
        DependencyProperty.RegisterAttached(
            "PopupPlacement",
            typeof(PlacementMode),
            typeof(CustomMenuHelper),
            new PropertyMetadata(PlacementMode.Bottom));

    public static void SetPopupPlacement(DependencyObject element, PlacementMode value)
    {
        element.SetValue(PopupPlacementProperty, value);
    }

    public static PlacementMode GetPopupPlacement(DependencyObject element)
    {
        return (PlacementMode)element.GetValue(PopupPlacementProperty);
    }
}