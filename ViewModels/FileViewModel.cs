using Microsoft.Win32;
using System.IO;
using System.Windows.Input;
using weirditor.Core;
using weirditor.Models;

namespace weirditor.ViewModels;

public class FileViewModel
{
    public DocumentModel Document { get; }

    //Toolbar commands
    public ICommand NewCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand SaveAsCommand { get; }
    public ICommand OpenCommand { get; }

    public FileViewModel(DocumentModel document)
    {
        Document = document;
        NewCommand = new RelayCommand(NewFile);
        SaveCommand = new RelayCommand(SaveFile, () => !Document.IsSaved);
        SaveAsCommand = new RelayCommand(SaveFileAs);
        OpenCommand = new RelayCommand(OpenFile);
    }

    public void NewFile()
    {
        Document.FileName = string.Empty;
        Document.FilePath = string.Empty;
        Document.TextEditor.Text = string.Empty;
    }

    private void SaveFile()
    {
        File.WriteAllText(Document.FilePath, Document.TextEditor.Text);
    }

    private void SaveFileAs()
    {
        var saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "Text File (*.txt)|*.txt";
        if(saveFileDialog.ShowDialog() == true)
        {
            DockFile(saveFileDialog);
            File.WriteAllText(saveFileDialog.FileName, Document.TextEditor.Text);
        }
    }

    private void OpenFile()
    {
        var openFileDialog = new OpenFileDialog();
        if (openFileDialog.ShowDialog() == true)
        {
            DockFile(openFileDialog);
            Document.TextEditor.Text = File.ReadAllText(openFileDialog.FileName);
        }
    }

    public void DockFile<T>(T dialog) where T : FileDialog
    {
        Document.FilePath = dialog.FileName;
        Document.FileName = dialog.SafeFileName;
    }
}
