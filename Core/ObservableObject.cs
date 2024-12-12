using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace weirditor.Core;

public class ObservableObject
{
    public event PropertyChangedEventHandler PropertyChanged;

    public void OnPropertyChanged<T>(ref T property, T value, [CallerMemberName] string propertyName = "")
    {
        property = value;
        var handler = PropertyChanged;
        if(handler != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}