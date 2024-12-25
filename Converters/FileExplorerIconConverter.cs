using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;
using FontAwesome.WPF;

namespace weirditor.Converters
{
    public class FileExplorerIconConverter : IValueConverter
    { 
        private Dictionary<string, Dictionary<string, object>> defaultDictionary =
            new Dictionary<string, Dictionary<string, object>>()
            {
                {
                    "Folder",
                    new Dictionary<string, object> { { "Icon", FontAwesomeIcon.Folder }, { "Color", Brushes.Khaki } }
                },
                {
                    "File",
                    new Dictionary<string, object> { { "Icon", FontAwesomeIcon.FileOutline }, { "Color", Brushes.Gray } }
                },
            };

        private Dictionary<string[], Dictionary<string, object>> extensionGroups =
            new Dictionary<string[], Dictionary<string, object>>()
            {
                {
                    new[] { ".txt" },
                    new Dictionary<string, object>
                        { { "Icon", FontAwesomeIcon.FileTextOutline }, { "Color", Brushes.Gray } }
                },
                {
                    new[] { ".png", ".jpg", ".jpeg" },
                    new Dictionary<string, object>
                        { { "Icon", FontAwesomeIcon.FileImageOutline }, { "Color", Brushes.LightSkyBlue } }
                },
                {
                    new[] { ".zip" },
                    new Dictionary<string, object>
                        { { "Icon", FontAwesomeIcon.FileZipOutline }, { "Color", Brushes.DarkMagenta } }
                },
                {
                    new[] { ".pdf" },
                    new Dictionary<string, object>
                        { { "Icon", FontAwesomeIcon.FilePdfOutline }, { "Color", Brushes.IndianRed } }
                },
                {
                    new[] { ".xls", ".xlsx" },
                    new Dictionary<string, object>
                        { { "Icon", FontAwesomeIcon.FileExcelOutline }, { "Color", Brushes.LightGreen } }
                },
                {
                    new[] { ".doc", ".docx" },
                    new Dictionary<string, object>
                        { { "Icon", FontAwesomeIcon.FileWordOutline }, { "Color", Brushes.DodgerBlue } }
                },
                {
                    new[] { ".mp4", ".avi", ".mkv", ".mov" },
                    new Dictionary<string, object>
                        { { "Icon", FontAwesomeIcon.FileVideoOutline }, { "Color", Brushes.LightSeaGreen } }
                },
                {
                    new[] { ".ppt", ".pptx" },
                    new Dictionary<string, object>
                        { { "Icon", FontAwesomeIcon.FilePowerpointOutline }, { "Color", Brushes.Coral } }
                },
                {
                    new[] { ".mp3", ".wav", ".flac" },
                    new Dictionary<string, object>
                        { { "Icon", FontAwesomeIcon.FileAudioOutline }, { "Color", Brushes.DarkGoldenrod } }
                },
                {
                    new[] { ".cs", ".cpp", ".c", ".h", ".java", ".py", ".js", ".html", ".css", ".xml", ".go", ".rs" },
                    new Dictionary<string, object>
                        { { "Icon", FontAwesomeIcon.FileCodeOutline }, { "Color", Brushes.MediumPurple } }
                },
            };

        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string path = value as string;
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }


            if (Directory.Exists(path))
            {
                return defaultDictionary["Folder"][parameter as string];
            }
            var extension = Path.GetExtension(path);

            foreach (var group in extensionGroups)
            {
                if (group.Key.Contains(extension))
                {
                    return group.Value[parameter as string];
                }
            }

            return defaultDictionary["File"][parameter as string];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack is not supported for FileExplorerIconConverter.");
        }
    }
}