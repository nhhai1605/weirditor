using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using weirditor.Core;
using weirditor.Models;

namespace weirditor.ViewModels;

public class DocumentViewModel
{
    public DocumentModel Document { get; set; }

    public DocumentViewModel()
    {
        Document = new DocumentModel();
    }
    
    public DocumentModel GetDocumentModel()
    {
        return Document;
    }
}

