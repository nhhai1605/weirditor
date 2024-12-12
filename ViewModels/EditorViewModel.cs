using System.Windows;
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
        var fontDialog = new FontDialog();
        fontDialog.DataContext = this;
        fontDialog.ShowDialog();
    }

    private void ToggleWrap()
    {
        Format.Wrap = Format.Wrap == TextWrapping.Wrap ? TextWrapping.NoWrap : TextWrapping.Wrap;
        MessageBox.Show("Wrap: " + Format.Wrap);
    }
}

