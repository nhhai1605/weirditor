using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ICSharpCode.AvalonEdit;
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
}

