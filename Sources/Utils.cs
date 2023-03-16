using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace BugFablesSaveEditor;

public static class Utils
{
  public enum ReorderDirection
  {
    Up,
    Down
  }

  public const string PrimarySeparator = ",";
  public const string SecondarySeparator = "@";

  public static Window MainWindow =>
    ((IClassicDesktopStyleApplicationLifetime)Application.Current?.ApplicationLifetime!)
    .MainWindow!;
}
