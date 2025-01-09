using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using weirditor.Core;

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

    public void SetBreadcrumb(string path)
    {
        BreadcrumbItems.Clear();
        if (string.IsNullOrEmpty(path))
        {
            return;
        }
        var pathParts = path.Split('\\');
        // only take after name of the parent folder
        for (int i = 0; i < pathParts.Length; i++)
        {
            BreadcrumbItems.Add(new BreadcrumbItem
            {
                Text = pathParts[i],
                IsLastItem = i == pathParts.Length - 1,
                Children = new ObservableCollection<BreadcrumbItem>
                {
                    new BreadcrumbItem
                    {
                        Text = "Child 1",
                        IsLastItem = false
                    },
                    new BreadcrumbItem
                    {
                        Text = "Child 2",
                        IsLastItem = false
                    }
                }
            });
        }
    }
}

public class BreadcrumbItem : ObservableObject
{
    public string Text { get; set; }
    public string PopupContent { get; set; }
    public bool IsLastItem { get; set; }
    private bool _isPopupOpen;

    public bool IsPopupOpen
    {
        get  { return _isPopupOpen; }
        set
        {
            OnPropertyChanged(ref _isPopupOpen, value);
            
        }
    }
    public ObservableCollection<BreadcrumbItem> Children { get; set; } // For hierarchical structure
    
    public void Children_OnClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Clicked");
    }
}
