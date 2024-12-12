using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using weirditor.Core;
using weirditor.Models;

namespace weirditor.ViewModels;

public class FileViewModel
{
    public EditorModel Editor { get; private set; }

    //Toolbar commands
    public ICommand NewCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand SaveAsCommand { get; }
    public ICommand OpenCommand { get; }

    public FileViewModel(EditorModel editor)
    {
        Editor = editor;
        NewCommand = new RelayCommand(NewFile);
        SaveCommand = new RelayCommand(SaveFile, () => !Editor.IsSaved);
        SaveAsCommand = new RelayCommand(SaveFileAs);
        OpenCommand = new RelayCommand(OpenFile);
    }

    public void NewFile()
    {
        Editor.FileName = string.Empty;
        Editor.FilePath = string.Empty;
        Editor.Text = string.Empty;
    }

    private void SaveFile()
    {
        File.WriteAllText(Editor.FilePath, Editor.Text);
    }

    private void SaveFileAs()
    {
        var saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "Text File (*.txt)|*.txt";
        if(saveFileDialog.ShowDialog() == true)
        {
            DockFile(saveFileDialog);
            File.WriteAllText(saveFileDialog.FileName, Editor.Text);
        }
    }

    private void OpenFile()
    {
        var openFileDialog = new OpenFileDialog();
        if (openFileDialog.ShowDialog() == true)
        {
            DockFile(openFileDialog);
            Editor.Text = File.ReadAllText(openFileDialog.FileName);
        }
    }

    private void DockFile<T>(T dialog) where T : FileDialog
    {
        Editor.FilePath = dialog.FileName;
        Editor.FileName = dialog.SafeFileName;
    }
}
