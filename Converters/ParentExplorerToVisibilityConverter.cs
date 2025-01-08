using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using FontAwesome.WPF;
using weirditor.Models;

namespace weirditor.Converters
{
    public class ParentExplorerToVisibilityConverter : IValueConverter
    { 
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ObservableCollection<ExplorerModel> parentExplorer && parentExplorer.Count > 0 && !string.IsNullOrEmpty(parentExplorer[0].Path))
            {
                // You can also add additional checks like if the path is not empty.
                return Visibility.Visible;
            }
            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack is not implemented.");
        }
    }
}