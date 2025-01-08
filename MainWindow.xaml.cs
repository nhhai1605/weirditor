using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using weirditor.Models;
using weirditor.ViewModels;

namespace weirditor;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private MainWindowViewModel MainWindowView;
    public MainWindow()
    {
        InitializeComponent();
        MainWindowView = (MainWindowViewModel) DataContext;
        MainWindowView.EditorTabControl = EditorTabControl;
        MainWindowView.BreadcrumbBarControl = BreadcrumbBarControl;
    }
    protected override void OnClosing(CancelEventArgs e)
    {
        if(MainWindowView.DocumentViewList.Where(d => !d.Document.IsSaved).Count() > 0)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to save changes?", "Save Changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                MainWindowView.SaveAllCommand.Execute(null);
            }
            else if(result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }
        base.OnClosing(e);
    }
    
    private void ExplorerTree_SelectedItemChanged(object sender, MouseButtonEventArgs e)
    {
        if (sender is TreeViewItem treeViewItem && treeViewItem.DataContext is ExplorerModel selectedItem)
        {
            MainWindowView?.ExplorerTree_SelectedItemChangedCommand.Execute(selectedItem);
        }
    }

    private void ExplorerTree_ItemRightClicked(object sender, MouseButtonEventArgs e)
    {
        if (sender is TreeViewItem treeViewItem && treeViewItem.DataContext is ExplorerModel selectedItem)
        {
            if (selectedItem != null)
            {
                selectedItem.IsSelected = true;
            }
        }
    }

    private void ExplorerTree_ItemDelete(object sender, RoutedEventArgs e)
    {
        ExplorerModel selectedItem = (ExplorerModel) ((MenuItem) sender).DataContext;
        MainWindowView.ExplorerTree_ItemDeleteCommand.Execute(selectedItem);
    }

    private void Menu_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.Source is Menu)
        {
            DragMove();
            e.Handled = true;
        }
    }
    
    private void Menu_CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void Menu_MinimizeButton_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void Menu_MaximizeButton_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState == WindowState.Maximized ? WindowState.Normal  : WindowState.Maximized; 
    }
}