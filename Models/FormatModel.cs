using System.Windows;
using System.Windows.Media;
using weirditor.Core;

namespace weirditor.Models;

public class FormatModel : ObservableObject
{
    private FontStyle _style;
    public FontStyle Style
    {
        get { return _style; }
        set { OnPropertyChanged(ref _style, value); }
    }

    private FontWeight _weight;
    public FontWeight Weight
    {
        get { return _weight; }
        set { OnPropertyChanged(ref _weight, value); }
    }

    private FontFamily _family;
    public FontFamily Family
    {
        get { return _family; }
        set { OnPropertyChanged(ref _family, value); }
    }

    private bool _wrap;
    public bool Wrap
    {
        get { return _wrap; }
        set
        {
            OnPropertyChanged(ref _wrap, value);
        }
    }

    private double _size;
    public double Size
    {
        get { return _size; }
        set { OnPropertyChanged(ref _size, value); }
    }
    
    private Brush _background;
    public Brush Background
    {
        get { return _background; }
        set { OnPropertyChanged(ref _background, value); }
    }
    
    private Brush _foreground;
    public Brush Foreground
    {
        get { return _foreground; }
        set { OnPropertyChanged(ref _foreground, value); }
    }
    
    private Brush _lineNumbersForeground;
    public Brush LineNumbersForeground
    {
        get { return _lineNumbersForeground; }
        set { OnPropertyChanged(ref _lineNumbersForeground, value); }
    } 
    
    private Brush _placeholderForeground;
    public Brush PlaceholderForeground
    {
        get { return _placeholderForeground; }
        set { OnPropertyChanged(ref _placeholderForeground, value); }
    }
    
        
    private Themes _theme;
    public Themes Theme
    {
        get { return _theme; }
        set
        {
            OnPropertyChanged(ref _theme, value);
            if(value == Themes.Light)
            {
                Background = Brushes.White;
                Foreground = Brushes.Black;
                LineNumbersForeground = Brushes.Gray;
                PlaceholderForeground = Brushes.Gray;
            }
            else
            {
                Background = Brushes.MidnightBlue;
                Foreground = Brushes.White;
                LineNumbersForeground = Brushes.LightGray;
                PlaceholderForeground = Brushes.LightGray;
            }
        }
    }
}

