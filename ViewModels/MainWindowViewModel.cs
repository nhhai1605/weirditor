using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weirditor.Models;

namespace weirditor.ViewModels;

public class MainWindowViewModel
{
    private EditorModel Editor;
    public EditorViewModel EditorView { get; set; }
    // public FileViewModel File { get; set; }

    public MainWindowViewModel()
    {
        EditorView = new EditorViewModel();
        Editor = EditorView.GetEditorModel();
        // File = new FileViewModel(_document);
    }
}

