using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using ICSharpCode.AvalonEdit;
using weirditor.Core;

namespace weirditor.Models;

public class ExplorerSettingModel : ObservableObject
{
    private GridLength _explorerColumnWidth = new GridLength(Constants.ExplorerWidth);
    public GridLength ExplorerColumnWidth
    {
        get => _explorerColumnWidth;
        set => OnPropertyChanged(ref _explorerColumnWidth, value);
    }
}

