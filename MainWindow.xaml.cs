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
    private MainWindowViewModel MainWindowViewModel;
    public MainWindow()
    {
        InitializeComponent();
        MainWindowViewModel = (MainWindowViewModel) DataContext;
        MainWindowViewModel.EditorTabControl = EditorTabControl;
    }
    protected override void OnClosing(CancelEventArgs e)
    {
        if(MainWindowViewModel.DocumentViewList.Where(d => !d.Document.IsSaved).Count() > 0)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to save changes?", "Save Changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                MainWindowViewModel.SaveAllCommand.Execute(null);
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
            MainWindowViewModel?.ExplorerTree_SelectedItemChangedCommand.Execute(selectedItem);
        }
    }
}