using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;

namespace BugFablesSaveEditor;

public static class Utils
{
  public static readonly FilePickerFileType SaveFileFilter = new("Bug Fables save (.dat)")
  {
    Patterns = new[] { "*.dat" }
  };

  public const string DialogSessionName = "Dialog";

  public static IPlatformSpecifics PlatformSpecifics { get; set; } = new PlatformSpecificImpl();

  public static TopLevel TopLevel
  {
    get
    {
      var lifetime = Application.Current?.ApplicationLifetime!;
      if (lifetime is IClassicDesktopStyleApplicationLifetime)
        return ((IClassicDesktopStyleApplicationLifetime)lifetime).MainWindow!;
      if (lifetime is ISingleViewApplicationLifetime singleViewPlatform)
        return TopLevel.GetTopLevel(singleViewPlatform.MainView)!;
      return null!;
    }
  }
}
