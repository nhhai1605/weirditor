using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace weirditor.Controls;

public partial class BreadcrumbBar : UserControl
{
    public BreadcrumbBar()
    {
        InitializeComponent();
        BreadcrumbItems = new ObservableCollection<BreadcrumbItem>();
        DataContext = this;
    }

    // Define BreadcrumbItems as a DependencyProperty
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
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        // Close all other popups
        foreach (var item in BreadcrumbItems)
        {
            item.IsPopupOpen = false;
        }

        // Open the clicked popup
        if (sender is Button button && button.DataContext is BreadcrumbItem itemContext)
        {
            itemContext.IsPopupOpen = true;
        }
    }

    public void SetBreadcrumb(string path)
    {
        BreadcrumbItems.Clear();
        var pathParts = path.Split('\\');
        // only take after name of the parent folder
        for (int i = 0; i < pathParts.Length; i++)
        {
            BreadcrumbItems.Add(new BreadcrumbItem
            {
                Text = pathParts[i],
                IsLastItem = i == pathParts.Length - 1
            });
        }
    }
}

public class BreadcrumbItem : INotifyPropertyChanged
{
    private bool _isPopupOpen;

    public string Text { get; set; }
    public string PopupContent { get; set; }
    public bool IsLastItem { get; set; }

    public bool IsPopupOpen
    {
        get => _isPopupOpen;
        set
        {
            _isPopupOpen = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
