using System.Runtime.Versioning;
using Avalonia;
using Avalonia.Browser;
using BugFablesSaveEditor.Core;
using Projektanker.Icons.Avalonia;
using Projektanker.Icons.Avalonia.MaterialDesign;

[assembly: SupportedOSPlatform("browser")]

namespace BugFablesSaveEditor.Browser;

internal class Program
{
  private static void Main(string[] args)
  {
    Utils.PlatformSpecifics = new BrowserPlatformSpecifics();
    BuildAvaloniaApp().StartBrowserAppAsync("out");
  }

  public static AppBuilder BuildAvaloniaApp()
  {
    IconProvider.Current.Register<MaterialDesignIconProvider>();

    return AppBuilder
      .Configure<App>()
      .WithInterFont();
  }
}
