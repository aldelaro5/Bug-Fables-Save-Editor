using System.Runtime.Versioning;
using Avalonia;
using Avalonia.Browser;
using BugFablesSaveEditor;
using Projektanker.Icons.Avalonia;
using Projektanker.Icons.Avalonia.MaterialDesign;

[assembly: SupportedOSPlatform("browser")]

internal partial class Program
{
  private static void Main(string[] args) => BuildAvaloniaApp()
    .SetupBrowserAppAsync();

  public static AppBuilder BuildAvaloniaApp()
    => AppBuilder
      .Configure<App>()
      .WithIcons(container => container
        .Register<MaterialDesignIconProvider>());
}
