using ICSharpCode.AvalonEdit;
using weirditor.Core;

namespace weirditor.Models;

public class DocumentModel : ObservableObject
{
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
    
    private string _initText;
    public string InitText
    {
        get { return _initText; }
        set { OnPropertyChanged(ref _initText, value); }
    }

    private bool _isNew;
    public bool IsNew
    {
        get { return _isNew; }
        set { OnPropertyChanged(ref _isNew, value); }
    }
    
    private bool _isSaved;
    public bool IsSaved
    {
        get { return _isSaved; }
        set { OnPropertyChanged(ref _isSaved, value); }
    }
}

