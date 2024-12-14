using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using ICSharpCode.AvalonEdit;
using Microsoft.Win32;
using weirditor.Core;
using weirditor.Models;

namespace weirditor.ViewModels;

public class MainWindowViewModel
{
    public TabControl? EditorTabControl { get; set; }
    public List<DocumentViewModel> DocumentViewList { get; set; }
    public int? FocusedDocumentIndex { get; set; }
    public DocumentViewModel? FocusedDocumentView
    {
        get => FocusedDocumentIndex.HasValue ? DocumentViewList[FocusedDocumentIndex.Value] : null;
        set => FocusedDocumentIndex = value != null ? DocumentViewList.IndexOf(value) : (int?)null;
    }
    public FormatModel Format { get; set; }
    public ICommand FormatCommand { get; }
    public ICommand WrapCommand { get; }
    public ICommand NewCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand SaveAsCommand { get; }
    public ICommand OpenCommand { get; }

    public MainWindowViewModel()
    {
        DocumentViewList = new List<DocumentViewModel>();
        Format = new FormatModel
        {
            Style = Config.DefaultFontStyle,
            Weight = Config.DefaultFontWeight,
            Family = Config.DefaultFontFamily,
            Size = Config.DefaultFontSize,
            Wrap = Config.DefaultWrap
        };
        FormatCommand = new RelayCommand(OpenStyleDialog);
        WrapCommand = new RelayCommand(ToggleWrap);
        NewCommand = new RelayCommand(NewFile);
        SaveCommand = new RelayCommand(SaveFile);
        SaveAsCommand = new RelayCommand(SaveFileAs);
        OpenCommand = new RelayCommand(OpenFile);
    }
    private void OpenStyleDialog()
    {
        var fontDialog = new FontDialog();
        fontDialog.DataContext = this;
        fontDialog.ShowDialog();
    }

    private void ToggleWrap()
    {
        Format.Wrap = !Format.Wrap;
    }
    
    public void NewFile()   
    {
        var documentView = new DocumentViewModel();
        DocumentViewList.Add(documentView);
        FocusedDocumentView = documentView;

        TextBlock footer = new TextBlock
        {
            Text = Config.NewFileText,
            FontSize = Config.DefaultFooterFontSize
        };
        footer.SetBinding(TextBlock.FontFamilyProperty, new Binding("Family") { Source = Format });

        TextEditor textEditor = new TextEditor
        {
            Name = "TextEditor" + FocusedDocumentIndex,
            ShowLineNumbers = true,
            LineNumbersForeground = Brushes.Gray
        };
        textEditor.SetBinding(TextEditor.FontFamilyProperty, new Binding("Family") { Source = Format });
        textEditor.SetBinding(TextEditor.FontSizeProperty, new Binding("Size") { Source = Format });
        textEditor.SetBinding(TextEditor.FontStyleProperty, new Binding("Style") { Source = Format });
        textEditor.SetBinding(TextEditor.FontWeightProperty, new Binding("Weight") { Source = Format });
        textEditor.SetBinding(TextEditor.WordWrapProperty, new Binding("Wrap") { Source = Format });

        DockPanel.SetDock(footer, Dock.Bottom);
        DockPanel dockPanel = new DockPanel();
        dockPanel.Children.Add(footer);
        dockPanel.Children.Add(textEditor);

        TabItem tabItem = new TabItem
        {
            IsSelected = true
        };

        TextBlock headerTextBlock = new TextBlock
        {
            Text = Config.NewFileText,
            Foreground = Brushes.Green,
            FontStyle = FontStyles.Italic
        };

        tabItem.Header = headerTextBlock;
        tabItem.Content = dockPanel;

        EditorTabControl?.Items.Add(tabItem);
    }

    private void SaveFile()
    {
        // File.WriteAllText(FocusedDocumentView.Document.FilePath, FocusedDocumentView.Document.TextEditor.Text);
    }

    private void SaveFileAs()
    {
        // var saveFileDialog = new SaveFileDialog();
        // saveFileDialog.Filter = "Text File (*.txt)|*.txt";
        // if(saveFileDialog.ShowDialog() == true)
        // {
        //     DockFile(saveFileDialog);
        //     File.WriteAllText(saveFileDialog.FileName, FocusedDocumentView.Document.TextEditor.Text);
        // }
    }

    private void OpenFile()
    {
        // var openFileDialog = new OpenFileDialog();
        // if (openFileDialog.ShowDialog() == true)
        // {
        //     DockFile(openFileDialog);
        //     FocusedDocumentView.Document.TextEditor.Text = File.ReadAllText(openFileDialog.FileName);
        // }
    }

    public void DockFile<T>(T dialog) where T : FileDialog
    {
        // FocusedDocumentView.Document.FilePath = dialog.FileName;
        // FocusedDocumentView.Document.FileName = dialog.SafeFileName;
    }
}

