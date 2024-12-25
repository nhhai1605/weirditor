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

    public ExplorerModel ParentExplorer { get; set; }
    public ExplorerSettingModel ExplorerSetting { get; set; }
    
    public ExplorerViewModel()
    {
        ParentExplorer = new ExplorerModel(String.Empty);
        ExplorerSetting = new ExplorerSettingModel();
    }
    
    public void ParentExplorerLoadDirectory(string path)
    {
        Mouse.OverrideCursor = Cursors.Wait;
        ParentExplorer.LoadDirectory(path);
        ParentExplorer.StartWatching();
        Mouse.OverrideCursor = Cursors.Arrow;
    }
}

