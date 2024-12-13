using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weirditor.Models;

namespace weirditor.ViewModels;

public class MainWindowViewModel
{
    public DocumentViewModel DocumentView { get; set; }
    public FileViewModel FileView { get; set; }

    public MainWindowViewModel()
    {
        DocumentView = new DocumentViewModel();
        FileView = new FileViewModel(DocumentView.GetDocumentModel());
    }
}

