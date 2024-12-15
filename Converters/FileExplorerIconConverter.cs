using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media;
using FontAwesome.WPF;

namespace weirditor.Converters;

public class FileExplorerIconConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string path = value as string;
        if (string.IsNullOrEmpty(path)) return (FontAwesomeIcon.File, Brushes.LightGray);  // Default to file icon
        var icon = Directory.Exists(path) ? FontAwesomeIcon.Folder : FontAwesomeIcon.File;
        var color = Directory.Exists(path) ?  Brushes.Khaki : Brushes.LightGray; // Yellowish for folder, white for file
        if ((string)parameter == "Icon")
        {
            return icon;
        }
        else if ((string)parameter == "Color")
        {
            return color;
        }
        else
        {
            throw new NotSupportedException("This parameter is not supported.");
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException("ConvertBack is not supported for FileExplorerIconConverter.");
    }
}