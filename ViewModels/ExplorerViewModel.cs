using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Highlighting;
using Microsoft.Win32;
using weirditor.Core;
using weirditor.Models;

namespace weirditor.ViewModels;

public class ExplorerViewModel : ObservableObject
{
    public ObservableCollection<ExplorerModel> ParentExplorer { get; set; }
    
    public ExplorerSettingModel ExplorerSetting { get; set; }
    
    public ExplorerViewModel()
    {
        ParentExplorer = new ObservableCollection<ExplorerModel>();
        ParentExplorer.Add(new ExplorerModel());
        ExplorerSetting = new ExplorerSettingModel();
    }
    
    public void ParentExplorerLoadDirectory(string path)
    {
        Mouse.OverrideCursor = Cursors.Wait;
        ExplorerModel? parentExplorer = ParentExplorer.FirstOrDefault();
        parentExplorer?.LoadDirectory(path);
        parentExplorer?.StartWatching();
        Mouse.OverrideCursor = Cursors.Arrow;
        //Need this to work because we don't actually change the object, we just load direction by updating path and children
        OnPropertyChanged(nameof(ParentExplorer));

    }
}

