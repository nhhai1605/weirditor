using System.Collections.ObjectModel;
using System.IO;
using ICSharpCode.AvalonEdit;
using weirditor.Core;

namespace weirditor.Models;

public class ExplorerSettingModel : ObservableObject
{
    private bool _isExplorerVisible = true;
    public bool IsExplorerVisible
    {
        get => _isExplorerVisible;
        set => OnPropertyChanged(ref _isExplorerVisible, value);
    }
}

