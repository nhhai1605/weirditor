﻿- Use styles for any styling of any components with helpers for custom properties (In /Styles and /Helpers folder)
- Use user controls for any custom components contain other components and custom properties as well (In /Controls folder). When making user control, we need DependencyProperty for custom properties. For example: 
```csharp
    public static readonly DependencyProperty BreadcrumbItemsProperty =
        DependencyProperty.Register(
            nameof(BreadcrumbItems),
            typeof(ObservableCollection<BreadcrumbItem>),
            typeof(BreadcrumbBar),
            new PropertyMetadata(new ObservableCollection<BreadcrumbItem>()));

    public ObservableCollection<BreadcrumbItem> BreadcrumbItems
    {
        get => (ObservableCollection<BreadcrumbItem>)GetValue(BreadcrumbItemsProperty);
        set => SetValue(BreadcrumbItemsProperty, value);
    }
```
- Extend class ObservableObject for NotifyPropertyChanged
- Use below to wait for the control is fully initialized before adding any action
```csharp
    Application.Current.Dispatcher.BeginInvoke(new Action(() =>
    {
        // Add your action here
    }), System.Windows.Threading.DispatcherPriority.Loaded);
```
- Use below to set visibility based on True and False without converter (sometime we don't want simple converter BoolToVisibilityConverter)
```xaml
    <TextBlock Text=" > " Foreground="{StaticResource TextBrush}">
        <TextBlock.Style>
            <Style TargetType="TextBlock">
                <Style.Triggers>
                    <DataTrigger Binding="{Binding IsLastItem}" Value="True">
                        <Setter Property="Visibility" Value="Collapsed" />
                    </DataTrigger>
                </Style.Triggers>
            </Style>
        </TextBlock.Style>
    </TextBlock>
```