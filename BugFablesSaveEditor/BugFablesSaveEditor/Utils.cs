using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace BugFablesSaveEditor;

public static class Utils
{
  public static Window MainWindow
  {
    get
    {
      var lifetime = Application.Current?.ApplicationLifetime!;
      if (lifetime is IClassicDesktopStyleApplicationLifetime desktop)
        return ((IClassicDesktopStyleApplicationLifetime)lifetime).MainWindow!;
      // TODO: Figure out how to do dialogs on browser
      // if (lifetime is ISingleViewApplicationLifetime singleViewPlatform)
      //   return ((ISingleViewApplicationLifetime)lifetime).MainView;
      return null!;
    }
  }
}
