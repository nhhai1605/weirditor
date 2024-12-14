using ICSharpCode.AvalonEdit;
using weirditor.Core;

namespace weirditor.Models;

public class DocumentModel : ObservableObject
{
    // private TextEditor _textEditor;
    // public TextEditor TextEditor
    // {
    //     get { return _textEditor; }
    //     set { OnPropertyChanged(ref _textEditor, value); }
    // }
    
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

    public bool IsNew
    {
        get
        {
            return string.IsNullOrEmpty(FileName) || string.IsNullOrEmpty(FilePath);
        }
    }
    private bool _isSaved;
    public bool IsSaved
    {
        get { return _isSaved; }
        set { OnPropertyChanged(ref _isSaved, value); }
    }
}

