using ICSharpCode.AvalonEdit;
using weirditor.Core;

namespace weirditor.Models;

public class DocumentModel : ObservableObject
{
    private TextEditor _textEditor;
    public TextEditor TextEditor
    {
        get { return _textEditor; }
        set { OnPropertyChanged(ref _textEditor, value); }
    }
    
    private string _filePath;
    public string FilePath
    {
        get { return _filePath; }
        set { OnPropertyChanged(ref _filePath, value); }
    }

    private string _fileName;
    public string FileName
    {
        get { return _fileName; }
        set { OnPropertyChanged(ref _fileName, value); }
    }

    public bool IsSaved
    {
        get
        {
            if (string.IsNullOrEmpty(FileName) ||
                string.IsNullOrEmpty(FilePath))
                return true;

            return false;
        }
    }
}

