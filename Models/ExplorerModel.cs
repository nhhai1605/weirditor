using System.Collections.ObjectModel;
using System.IO;
using ICSharpCode.AvalonEdit;
using weirditor.Core;

namespace weirditor.Models;

public class ExplorerModel : ObservableObject
{
    private string _name = string.Empty;
    public string Name
    {
        get { return _name; }
        set { OnPropertyChanged(ref _name, value); }
    }

    private string _path = string.Empty;
    public string Path
    {
        get { return _path; }
        set { OnPropertyChanged(ref _path, value); }
    }

    private ObservableCollection<ExplorerModel> _children;
    public ObservableCollection<ExplorerModel> Children
    {
        get { return _children; }
        set { OnPropertyChanged(ref _children, value); }
    }

    public ExplorerModel(string path)
    {
        Children = new ObservableCollection<ExplorerModel>();
        if (!string.IsNullOrEmpty(path))
        {
            LoadDirectory(path);
        }
    }

    public void LoadDirectory(string path)
    {
        Path = path;
        Name = System.IO.Path.GetFileName(path);
        if (Directory.Exists(path))
        {
            foreach (var dir in Directory.GetDirectories(path))
            {
                Children.Add(new ExplorerModel(dir));
            }
            foreach (var file in Directory.GetFiles(path))
            {
                Children.Add(new ExplorerModel(file));
            }
        }
    }
}

