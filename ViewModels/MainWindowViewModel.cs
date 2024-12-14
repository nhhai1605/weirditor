using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using weirditor.Core;
using weirditor.Models;

namespace weirditor.ViewModels;

public class MainWindowViewModel
{
    public DocumentViewModel DocumentView { get; set; }
    public FileViewModel FileView { get; set; }
    public ICommand FormatCommand { get; }
    public ICommand WrapCommand { get; }
    public FormatModel Format { get; set; }

    public MainWindowViewModel()
    {
        DocumentView = new DocumentViewModel();
        FileView = new FileViewModel(DocumentView.GetDocumentModel());
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

