using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Highlighting;
using Microsoft.Win32;
using weirditor.Core;
using weirditor.Models;

namespace weirditor.ViewModels;

public class ExplorerViewModel
{

    public ObservableCollection<ExplorerModel> ParentExplorer { get; set; }
    public ExplorerSettingModel ExplorerSetting { get; set; }
    
    public ExplorerViewModel()
    {
        ParentExplorer = new ObservableCollection<ExplorerModel>();
        ParentExplorer.Add(new ExplorerModel(String.Empty));
        ExplorerSetting = new ExplorerSettingModel();
    }
    
    public void ParentExplorerLoadDirectory(string path)
    {
        Mouse.OverrideCursor = Cursors.Wait;
        if(ParentExplorer.Count > 0)
        {
            ExplorerModel parentExplorer = ParentExplorer[0];
            parentExplorer.LoadDirectory(path);
            parentExplorer.StartWatching();
        }
        Mouse.OverrideCursor = Cursors.Arrow;
    }
}

