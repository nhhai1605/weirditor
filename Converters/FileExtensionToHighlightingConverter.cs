using System.Globalization;
using System.IO;
using System.Windows.Data;
using ICSharpCode.AvalonEdit.Highlighting;

namespace weirditor.Converters;

public class FileExtensionToHighlightingConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string filePath)
        {
            string extension = Path.GetExtension(filePath);
            return HighlightingManager.Instance.GetDefinitionByExtension(extension);
        }
        return null; // Default to no highlighting if the file path is invalid
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException("ConvertBack is not supported for FileExtensionToHighlightingConverter.");
    }
}