﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using weirditor.Core;
using weirditor.Models;

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

    public void SetBreadcrumb(string filePath, string parentDirectoryPath)
    {
        var relative = filePath.Split(parentDirectoryPath)[1];
        var parentDirectoryName = parentDirectoryPath.Split('\\').Last();
        var pathParts = (parentDirectoryName + relative).Split('\\');
        BreadcrumbItems.Clear();
        for (int i = 0; i < pathParts.Length; i++)
        {
            //Todo: get all files in the path
            var children = new ObservableCollection<BreadcrumbItem>();
            //if is file, get all functions and variables
            if (i == pathParts.Length - 1) // is file
            {
                //TODO: for now, just get all files in the directory, later we will get all functions and variables with tree-sitter or sth
                // foreach (string funcAndVar in ExtractFunctionsAndVariables(filePath))
                // {
                //     children.Add(new BreadcrumbItem
                //     {
                //         Text = funcAndVar,
                //     });
                // }
            }
            else
            {
                var directoryPath = string.Empty;
                if (pathParts[i] == parentDirectoryName)
                {
                    directoryPath = parentDirectoryPath;
                }
                else
                {
                    directoryPath = parentDirectoryPath + "\\" + string.Join("\\", pathParts.Take(i + 1).Skip(1));
                }
                foreach (string file in Directory.GetFiles(directoryPath))
                {
                    children.Add(new BreadcrumbItem
                    {
                        Text = Path.GetFileName(file),
                    });
                }  
                
            }
            BreadcrumbItems.Add(new BreadcrumbItem
            {
                Text = pathParts[i],
                IsLastItem = i == pathParts.Length - 1,
                IsEnabled = true,
                Children = children
            });
            //Now add a separator as a MenuItem that IsEnabled = false
            if(i < pathParts.Length - 1)
            {
                BreadcrumbItems.Add(new BreadcrumbItem
                {
                    Text = ">",
                    IsEnabled = false
                });
            }
        }
    }
    
    private List<string> ExtractFunctionsAndVariables(string filePath)
    {
        List<string> results = new();
        try
        {
            string fileContent = File.ReadAllText(filePath);
            string fileExtension = Path.GetExtension(filePath);
            string pattern = string.Empty;

            switch (fileExtension)
            {
                case ".cs":
                    pattern = @"(public|private|protected|internal|static)?\s*\w+\s+\w+\s*\(.*?\)";
                    break;
                case ".py":
                    pattern = @"def\s+\w+\s*\(.*?\):";
                    break;
                case ".cpp":
                case ".c":
                case ".h":
                case ".hpp":
                    pattern = @"\w+\s+\w+\s*\(.*?\)\s*{";
                    break;
                case ".java":
                    pattern = @"(public|private|protected|static)?\s*\w+\s+\w+\s*\(.*?\)";
                    break;
            }

            if (!string.IsNullOrEmpty(pattern))
            {
                Regex regex = new Regex(pattern);
                foreach (Match match in regex.Matches(fileContent))
                {
                    results.Add(match.Value.Trim());
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return results;
    }
}

public class BreadcrumbItem : ObservableObject
{
    public string Text { get; set; }
    public string PopupContent { get; set; }
    public bool IsLastItem { get; set; }
    public bool IsEnabled { get; set; }
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
