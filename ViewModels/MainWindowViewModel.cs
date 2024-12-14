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
        NewCommand = new RelayCommand(() => NewFile(null));
        SaveCommand = new RelayCommand(SaveFile, () => EditorTabControl?.SelectedIndex >= 0);
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
    
    public void NewFile(OpenFileDialog? openFileDialog)   
    {
        TextBlock footer = new TextBlock
        {
            Text = openFileDialog != null ? openFileDialog.FileName : Config.NewFileText,
            FontSize = Config.DefaultFooterFontSize
        };
        footer.SetBinding(TextBlock.FontFamilyProperty, new Binding("Family") { Source = Format });

        TextEditor textEditor = new TextEditor
        {
            ShowLineNumbers = true,
            LineNumbersForeground = Brushes.Gray
        };
        textEditor.SetBinding(TextEditor.FontFamilyProperty, new Binding("Family") { Source = Format });
        textEditor.SetBinding(TextEditor.FontSizeProperty, new Binding("Size") { Source = Format });
        textEditor.SetBinding(TextEditor.FontStyleProperty, new Binding("Style") { Source = Format });
        textEditor.SetBinding(TextEditor.FontWeightProperty, new Binding("Weight") { Source = Format });
        textEditor.SetBinding(TextEditor.WordWrapProperty, new Binding("Wrap") { Source = Format });
        
        var documentView = new DocumentViewModel(textEditor);
        if (openFileDialog != null)
        {
            DockFile(openFileDialog, documentView.Document);
            documentView.Document.IsSaved = true;
            textEditor.Text = File.ReadAllText(openFileDialog.FileName);
            documentView.Document.InitText = textEditor.Text;
            textEditor.CaretOffset = textEditor.Text.Length;
        }
        
        DockPanel.SetDock(footer, Dock.Bottom);
        DockPanel dockPanel = new DockPanel();
        dockPanel.Children.Add(footer);
        dockPanel.Children.Add(textEditor);

        TabItem tabItem = new TabItem
        {
            IsSelected = true
        };
        StackPanel header = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Children =
            {
                new TextBlock
                {
                    Text = openFileDialog != null ? openFileDialog.SafeFileName : Config.NewFileText,
                    // Foreground = openFileDialog != null ? Brushes.Black : Brushes.Green,
                    FontStyle = openFileDialog != null ? FontStyles.Normal : FontStyles.Italic,
                    Style = new Style(typeof(TextBlock))
                    {
                        Triggers =
                        {
                            // Trigger for Document.IsSaved
                            new DataTrigger
                            {
                                Binding = new Binding("IsSaved")
                                {
                                    Source = documentView.Document,
                                },
                                Value = false,
                                Setters =
                                {
                                    new Setter(TextBlock.ForegroundProperty, Brushes.Orange)
                                }
                            },
                            // Trigger for Document.IsNew
                            new DataTrigger
                            {
                                Binding = new Binding("IsNew")
                                {
                                    Source = documentView.Document,
                                },
                                Value = true,
                                Setters =
                                {
                                    new Setter(TextBlock.ForegroundProperty, Brushes.Green)
                                }
                            },
                            
                        },
                        Setters =
                        {
                            new Setter(TextBlock.ForegroundProperty, Brushes.Black)
                        }
                    }
                },
                new Button
                {
                    Content = " X ",
                    Margin = new Thickness(10, 0, 0, 0),
                    Padding = new Thickness(0),
                    BorderThickness = new Thickness(0),
                    Background = Brushes.Transparent,
                    Command = new RelayCommand(() => EditorTabControl?.Items.Remove(tabItem))
                }
            },
        };
        // Close tab with middle mouse button
        header.MouseUp += (_, e) =>
        {
            if (e.ChangedButton == MouseButton.Middle)
            {
                EditorTabControl?.Items.Remove(tabItem);
            }
        };
        tabItem.Header = header;
        tabItem.Content = dockPanel;
        
        
        EditorTabControl?.Items.Add(tabItem);
        DocumentViewList.Add(documentView);
        
        // Use this to wait for the UI to update before focusing the text editor
        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
        {
            documentView.TextEditor.Focus();
        }), System.Windows.Threading.DispatcherPriority.Input);
        
        textEditor.TextChanged += (_, _) =>
        {
            if(!documentView.Document.IsNew)
            {
                documentView.Document.IsSaved = textEditor.Text == documentView.Document.InitText;
            }
        };
    }

    private void SaveFile()
    {
        if (EditorTabControl?.SelectedIndex >= 0) //Make sure there is a selected tab
        {
            DocumentViewModel documentView = DocumentViewList[EditorTabControl.SelectedIndex];
            if (documentView.Document.IsNew)
            {
                SaveFileAs();
            }
            else
            {
                File.WriteAllText(documentView.Document.FilePath, documentView.TextEditor.Text);
            }
            
        }
    }

    private void SaveFileAs()
    {
        var saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "Text File (*.txt)|*.txt";
        if(saveFileDialog.ShowDialog() == true)
        {
            // DockFile(saveFileDialog);
            File.WriteAllText(saveFileDialog.FileName, DocumentViewList[EditorTabControl!.SelectedIndex].TextEditor.Text);
        }
    }

    private void OpenFile()
    {
        var openFileDialog = new OpenFileDialog();
        if (openFileDialog.ShowDialog() == true)
        {
            NewFile(openFileDialog);
        }
    }
    
    private void DockFile(OpenFileDialog dialog, DocumentModel document)
    {
        document.FilePath = dialog.FileName;
        document.FileName = dialog.SafeFileName;
    }
    
}

