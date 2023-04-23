using System.Runtime.Versioning;
using Avalonia;
using Avalonia.Browser;
using Avalonia.Fonts.Inter;
using BugFablesSaveEditor;
using BugFablesSaveEditor.Browser;
using Projektanker.Icons.Avalonia;
using Projektanker.Icons.Avalonia.MaterialDesign;

[assembly: SupportedOSPlatform("browser")]

internal class Program
{
  private static void Main(string[] args)
  {
    Utils.PlatformSpecifics = new BrowserPlatformSpecifics();
    BuildAvaloniaApp().StartBrowserAppAsync("out");
  }

  public static AppBuilder BuildAvaloniaApp()
    => AppBuilder
      .Configure<App>()
      .WithInterFont()
      .WithIcons(container => container
        .Register<MaterialDesignIconProvider>());
}
