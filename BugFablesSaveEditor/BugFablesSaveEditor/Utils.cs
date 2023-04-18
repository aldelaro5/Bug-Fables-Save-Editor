using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace BugFablesSaveEditor;

public static class Utils
{
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
