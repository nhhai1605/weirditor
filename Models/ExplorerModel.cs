using System.Collections.ObjectModel;
using System.IO;
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

    private bool _isSelected;
    public bool IsSelected
    {
        get { return _isSelected; }
        set { OnPropertyChanged(ref _isSelected, value); }
    }
    
    private FileSystemWatcher? _fileSystemWatcher;
    
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
        Children = new ObservableCollection<ExplorerModel>();
        if (Directory.Exists(path))
        {
            //Need to use Dispatcher.Invoke to update UI thread (https://stackoverflow.com/a/18336392)
            foreach (var dir in Directory.GetDirectories(path))
            {
                App.Current.Dispatcher.Invoke((Action) delegate 
                {
                    Children.Add(new ExplorerModel(dir));
                });
            }
            foreach (var file in Directory.GetFiles(path))
            {
                App.Current.Dispatcher.Invoke((Action) delegate 
                {
                    Children.Add(new ExplorerModel(file));
                });
            }
        }
    }
    public void StartWatching()
    {
        if (Directory.Exists(Path))
        {
            _fileSystemWatcher = new FileSystemWatcher(Path)
            {
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite
            };

            _fileSystemWatcher.Created += OnFileSystemChanged;
            _fileSystemWatcher.Deleted += OnFileSystemChanged;
            _fileSystemWatcher.Renamed += OnFileRenamed;
            _fileSystemWatcher.Changed += OnFileSystemChanged;

            _fileSystemWatcher.IncludeSubdirectories = true;
            _fileSystemWatcher.EnableRaisingEvents = true;
        }
    }

    private void StopWatching()
    {
        if (_fileSystemWatcher != null)
        {
            _fileSystemWatcher.EnableRaisingEvents = false;
            _fileSystemWatcher.Created -= OnFileSystemChanged;
            _fileSystemWatcher.Deleted -= OnFileSystemChanged;
            _fileSystemWatcher.Renamed -= OnFileRenamed;
            _fileSystemWatcher.Changed -= OnFileSystemChanged;
            _fileSystemWatcher.Dispose();
            _fileSystemWatcher = null;
        }
    }

    private void OnFileSystemChanged(object sender, FileSystemEventArgs e)
    {
        LoadDirectory(Path);
    }

    private void OnFileRenamed(object sender, RenamedEventArgs e)
    {
        LoadDirectory(Path);
    }

    ~ExplorerModel()
    {
        StopWatching();
    }
}

