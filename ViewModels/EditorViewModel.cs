using System.Windows.Input;
using weirditor.Core;
using weirditor.Models;

namespace weirditor.ViewModels;

public class EditorViewModel
{
    public ICommand FormatCommand { get; }
    public ICommand WrapCommand { get; }
    public FormatModel Format { get; set; }
    public EditorModel Editor { get; set; }

    public EditorViewModel()
    {
        Editor = new EditorModel();
        Format = new FormatModel();
        FormatCommand = new RelayCommand(OpenStyleDialog);
        WrapCommand = new RelayCommand(ToggleWrap);
    }
    
    public EditorModel GetEditorModel()
    {
        return Editor;
    }
    
    private void OpenStyleDialog()
    {
        // var fontDialog = new FontDialog();
        // fontDialog.DataContext = Format;
        // fontDialog.ShowDialog();
    }

    private void ToggleWrap()
    {
        if (Format.Wrap == System.Windows.TextWrapping.Wrap)
            Format.Wrap = System.Windows.TextWrapping.NoWrap;
        else
            Format.Wrap = System.Windows.TextWrapping.Wrap;
    }
}

