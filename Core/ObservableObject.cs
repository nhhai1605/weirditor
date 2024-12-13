using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace weirditor.Core;

public class ObservableObject: INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged<T>(ref T property, T value, [CallerMemberName] string propertyName = "")
    {
        property = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}