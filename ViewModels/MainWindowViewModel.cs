﻿using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Highlighting;
using Microsoft.Win32;
using weirditor.Converters;
using weirditor.Core;
using weirditor.Models;

namespace weirditor.ViewModels;

public class MainWindowViewModel
{
    public TabControl? EditorTabControl { get; set; }
    public List<DocumentViewModel> DocumentViewList { get; set; }
    public ExplorerViewModel ExploreView { get; set; }
    public FormatModel Format { get; set; }
    public ICommand FormatCommand { get; }
    public ICommand WrapCommand { get; }
    public ICommand NewCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand SaveAsCommand { get; }
    public ICommand SaveAllCommand { get; }
    public ICommand OpenCommand { get; }
    public ICommand LoadParentDirectoryCommand { get; }
    public ICommand ExplorerVisibilityCommand { get; } 
    public ICommand ExplorerTree_SelectedItemChangedCommand { get; } 
    
    public MainWindowViewModel()
    {
        DocumentViewList = new List<DocumentViewModel>();
        ExploreView = new ExplorerViewModel();
        Format = new FormatModel
        {
            Style = Config.DefaultFontStyle,
            Weight = Config.DefaultFontWeight,
            Family = Config.DefaultFontFamily,
            Size = Config.DefaultFontSize,
            Wrap = Config.DefaultWrap
        };
        FormatCommand = new RelayCommand((_) => OpenStyleDialog());
        WrapCommand = new RelayCommand((_) => ToggleWrap());
        NewCommand = new RelayCommand((_) => NewFile(null));
        SaveCommand = new RelayCommand((_) => SaveFile(), () => EditorTabControl?.SelectedIndex >= 0);
        SaveAsCommand = new RelayCommand((_) => SaveFileAs());
        OpenCommand = new RelayCommand((_) => OpenFile());
        SaveAllCommand = new RelayCommand((_) => SaveAll());
        LoadParentDirectoryCommand = new RelayCommand((_) => LoadParentDirectory());
        ExplorerVisibilityCommand = new RelayCommand((_) => ExplorerVisibilityControl());
        ExplorerTree_SelectedItemChangedCommand = new RelayCommand((parameter) => ExplorerTree_SelectedItemChanged(parameter));
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
    
    public void NewFile(string? path)   
    {
        TextEditor textEditor = new TextEditor
        {
            ShowLineNumbers = true,
            LineNumbersForeground = Brushes.Gray
        };
        var documentView = new DocumentViewModel(textEditor);

        textEditor.SetBinding(TextEditor.FontFamilyProperty, new Binding("Family") { Source = Format });
        textEditor.SetBinding(TextEditor.FontSizeProperty, new Binding("Size") { Source = Format });
        textEditor.SetBinding(TextEditor.FontStyleProperty, new Binding("Style") { Source = Format });
        textEditor.SetBinding(TextEditor.FontWeightProperty, new Binding("Weight") { Source = Format });
        textEditor.SetBinding(TextEditor.WordWrapProperty, new Binding("Wrap") { Source = Format });
        textEditor.SetBinding(TextEditor.SyntaxHighlightingProperty, new Binding("FilePath")
        {
            Source = documentView.Document,
            Converter = new FileExtensionToHighlightingConverter()
        });
        var fileName = string.Empty;
        if (!string.IsNullOrEmpty(path))
        {
            fileName = Path.GetFileName(path);
            documentView.DockFile(path);
            documentView.Document.IsNew = false;
            documentView.Document.IsSaved = true;
            textEditor.Load(path);
            documentView.Document.InitText = textEditor.Text;
            textEditor.CaretOffset = textEditor.Text.Length;
            textEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinitionByExtension(Path.GetExtension(path));
        }
        else
        {
            documentView.Document.IsNew  = true;
            documentView.Document.IsSaved = true;
            documentView.Document.FileName = Config.NewFileText;
            //TODO: Will change if create file in directory from file browser
            documentView.Document.FilePath = Config.NewFileText;
        }
        
        textEditor.TextChanged += (_, _) =>
        {
            documentView.Document.IsSaved = textEditor.Text == documentView.Document.InitText;
        };
        
        TextBlock footer = new TextBlock
        {
            FontSize = Config.DefaultFooterFontSize
        };
        footer.SetBinding(TextBlock.TextProperty, new Binding("FilePath") { Source = documentView.Document });
        footer.SetBinding(TextBlock.FontFamilyProperty, new Binding("Family") { Source = Format });
        
        DockPanel.SetDock(footer, Dock.Bottom);
        DockPanel dockPanel = new DockPanel();
        dockPanel.Children.Add(footer);
        dockPanel.Children.Add(textEditor);
        
        TabItem tabItem = new TabItem
        {
            IsSelected = true
        };
        
        //Add tabItem before adding CloseTab()
        EditorTabControl?.Items.Add(tabItem);
        DocumentViewList.Add(documentView);
        
        TextBlock headerTextBlock = new TextBlock
        {
            Text = string.IsNullOrEmpty(fileName) ? Config.NewFileText : fileName,
            Style = new Style(typeof(TextBlock))
            {
                Triggers =
                {
                    new DataTrigger
                    {
                        Binding = new Binding("IsSaved")
                        {
                            Source = documentView.Document,
                        },
                        Value = false,
                        Setters =
                        {
                            new Setter(TextBlock.ForegroundProperty, Brushes.Orange),
                            new Setter(TextBlock.FontStyleProperty, FontStyles.Normal)
                        }
                    },
                    new DataTrigger
                    {
                        Binding = new Binding("IsNew")
                        {
                            Source = documentView.Document,
                        },
                        Value = true,
                        Setters =
                        {
                            new Setter(TextBlock.ForegroundProperty, Brushes.Green),
                            new Setter(TextBlock.FontStyleProperty, FontStyles.Italic)
                        }
                    },

                },
                Setters =
                {
                    new Setter(TextBlock.ForegroundProperty, Brushes.Black),
                    new Setter(TextBlock.FontStyleProperty, FontStyles.Normal)
                }
            }
        };
        headerTextBlock.SetBinding(TextBlock.TextProperty, new Binding("FileName") { Source = documentView.Document });
        

        StackPanel header = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Children =
            {
                headerTextBlock,
                new Button
                {
                    Content = " X ",
                    Margin = new Thickness(10, 0, 0, 0),
                    Padding = new Thickness(0),
                    BorderThickness = new Thickness(0),
                    Background = Brushes.Transparent,
                    Command = new RelayCommand((_) => CloseTab(tabItem))
                }
            },
        };
        // Close tab with middle mouse button
        header.MouseUp += (_, e) =>
        {
            if (e.ChangedButton == MouseButton.Middle)
            {
                CloseTab(tabItem);
            }
        };
        tabItem.Header = header;
        tabItem.Content = dockPanel;
        
        // Use this to wait for the UI to update before focusing the text editor
        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
        {
            documentView.TextEditor.Focus();
        }), System.Windows.Threading.DispatcherPriority.Input);
    }
    
    private void CloseTab(TabItem tabItem)
    {
        var documentView = DocumentViewList[EditorTabControl!.Items.IndexOf(tabItem)];
        if (!documentView.Document.IsSaved)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to save changes?", "Save Changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                SaveFile();
            }
        }
        EditorTabControl?.Items.Remove(tabItem);
        DocumentViewList.Remove(documentView);
    }
    
    private void SaveFile()
    {
        if (EditorTabControl?.SelectedIndex >= 0) //Make sure there is a selected tab
        {
            DocumentViewModel documentView = DocumentViewList[EditorTabControl.SelectedIndex];
            if (documentView.Document.IsNew) {
                SaveFileAs();
            }
            else
            {
                documentView.TextEditor.Save(documentView.Document.FilePath);
                documentView.OnSavedDocument();
            }
        }
    }

    private void SaveAll()
    {
        foreach (var documentView in DocumentViewList)
        {
            if (!documentView.Document.IsSaved)
            {
                documentView.TextEditor.Save(documentView.Document.FilePath);
                documentView.OnSavedDocument();
            }
        }
    }

    private void SaveFileAs()
    {
        SaveFileDialog dialog = new SaveFileDialog();
        dialog.DefaultExt = ".txt";
        if (dialog.ShowDialog() == true) 
        {
            var documentView = DocumentViewList[EditorTabControl!.SelectedIndex];
            documentView.DockFile(dialog.FileName);
            documentView.TextEditor.Save(dialog.FileName);
            documentView.OnSavedDocument();
        }
    }

    private void OpenFile()
    {
        var openFileDialog = new OpenFileDialog();
        if (openFileDialog.ShowDialog() == true)
        {
            NewFile(openFileDialog.FileName);
        }
    }
    
    private void LoadParentDirectory()
    {
        var openFolderDialog = new OpenFolderDialog();
        if (openFolderDialog.ShowDialog() == true)
        {
            ExploreView.ParentExplorer.LoadDirectory(openFolderDialog.FolderName);
        }
    } 
    
    private void ExplorerVisibilityControl()
    {
        ExploreView.ExplorerSetting.IsExplorerVisible = !ExploreView.ExplorerSetting.IsExplorerVisible;
    } 
    
    private void ExplorerTree_SelectedItemChanged(object? parameter)
    {
        if (parameter != null && parameter is ExplorerModel selectedItem)
        {
            if (EditorTabControl!.Items.Count > 0)
            {
                var existedItem = DocumentViewList.Find(x => x.Document.FilePath == selectedItem.Path);
                if (existedItem != null)
                {
                    EditorTabControl.SelectedIndex = DocumentViewList.IndexOf(existedItem);
                    return;
                }
            }
            NewFile(selectedItem.Path);
        }
    }
    
}

