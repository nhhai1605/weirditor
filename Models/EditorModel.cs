using weirditor.Core;

namespace weirditor.Models;

public class EditorModel : ObservableObject
{
    private string _text;
    public string Text
    {
        get { return _text; }
        set { OnPropertyChanged(ref _text, value); }
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

