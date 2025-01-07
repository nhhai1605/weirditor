using System.Windows;

namespace weirditor.Core;

public enum Themes
{
    Light,
    Dark
}

public static class Constants
{
    public static readonly double ExplorerWidth = 200;
    public static readonly double ExplorerExpandSpeedRatio = 10;
    public static readonly Duration ExplorerExpandDuration = new Duration(TimeSpan.FromSeconds(1));
}