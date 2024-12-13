using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using weirditor.Core;
using weirditor.Models;

namespace weirditor.ViewModels;

public class DocumentViewModel
{
    public ICommand FormatCommand { get; }
    public ICommand WrapCommand { get; }
    public FormatModel Format { get; set; }
    public DocumentModel Document { get; set; }

    public DocumentViewModel()
    {
        Document = new DocumentModel();
        Format = new FormatModel
        {
            Style = FontStyles.Normal,
            Weight = FontWeights.Normal,
            Family = new FontFamily("Courier New"),
            Size = 18,
            Wrap = true
        };
        FormatCommand = new RelayCommand(OpenStyleDialog);
        WrapCommand = new RelayCommand(ToggleWrap);
    }
    
    public DocumentModel GetDocumentModel()
    {
        return Document;
    }
    
    private void OpenStyleDialog()
    {
        var fontDialog = new FontDialog();
        fontDialog.DataContext = this;
        fontDialog.ShowDialog();
    }

    private void ToggleWrap()
    {
        Format.Wrap = !Format.Wrap;
    }
}

