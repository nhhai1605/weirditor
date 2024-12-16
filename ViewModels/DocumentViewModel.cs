using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Highlighting;
using Microsoft.Win32;
using weirditor.Core;
using weirditor.Models;

namespace weirditor.ViewModels;

public class DocumentViewModel
{
    public DocumentModel Document { get; set; }
    public TextEditor TextEditor { get; set; }

    public DocumentViewModel(TextEditor _textEditor)
    {
        Document = new DocumentModel();
        TextEditor = _textEditor;
    }
    
    public void OnSavedDocument()
    {
        Document.IsSaved = true;
        Document.IsNew = false;
        Document.InitText = TextEditor.Text;
    }

    public void DockFile(string path)
    {
        var fileName = Path.GetFileName(path);
        Document.FilePath = path;
        Document.FileName = fileName;
    }
}

