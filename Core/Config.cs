using System.Windows;
using System.Windows.Media;

namespace weirditor.Core;

public class Config
{
    public static string NewFileText = "Untitled";
    public static FontFamily DefaultFontFamily = new FontFamily("Courier New");
    public static int DefaultFontSize = 18;
    public static bool DefaultWrap = false;
    public static FontStyle DefaultFontStyle = FontStyles.Normal;
    public static FontWeight DefaultFontWeight = FontWeights.Normal;
}