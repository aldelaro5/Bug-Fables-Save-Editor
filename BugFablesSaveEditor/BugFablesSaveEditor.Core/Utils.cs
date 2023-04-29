using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;

namespace BugFablesSaveEditor.Core;

public static class Utils
{
  public static readonly FilePickerFileType SaveFileFilter = new("Bug Fables save (.dat)")
  {
    Patterns = new[] { "*.dat" }
  };

  public const string DialogSessionName = "Dialog";

  public const string MessageXboxSave =
    "Place the file at the same location that you retrieved it making sure the filename match the existing one.\n" +
    "When the game will boot, all save files whose names do not match the existing ones will be deleted.";

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
