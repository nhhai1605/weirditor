using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
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
        MainWindowViewModel.DocumentView.Document.TextEditor = TextEditor;
    }
}