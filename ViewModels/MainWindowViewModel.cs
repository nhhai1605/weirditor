using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weirditor.Models;

namespace weirditor.ViewModels;

public class MainWindowViewModel
{
    public EditorViewModel EditorView { get; set; }
    public FileViewModel FileView { get; set; }

    public MainWindowViewModel()
    {
        EditorView = new EditorViewModel();
        FileView = new FileViewModel(EditorView.GetEditorModel());
    }
}

