- Use styles for any styling of any components with helpers for custom properties (In /Styles and /Helpers folder)
- Use user controls for any custom components contain other components and custom properties as well (In /Controls folder)
- Extend class ObservableObject for NotifyPropertyChanged
- Use below to wait for the control is fully initialized before adding any action
```csharp
        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
        {
            // Add your action here
        }), System.Windows.Threading.DispatcherPriority.Loaded);
```