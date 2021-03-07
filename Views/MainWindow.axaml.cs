using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave;
using System.Runtime.InteropServices;

namespace BugFablesSaveEditor.Views
{
  public class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
#if DEBUG
      this.AttachDevTools();
#endif

      if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
      {
        ExtendClientAreaToDecorationsHint = true;
        ExtendClientAreaChromeHints = Avalonia.Platform.ExtendClientAreaChromeHints.NoChrome;
        ExtendClientAreaTitleBarHeightHint = -1;

        var windowsTitleBar = this.FindControl<WindowsTitleBar>("windowsTitleBar");
        windowsTitleBar.IsVisible = true;
      }
    }

    private void InitializeComponent()
    {
      AvaloniaXamlLoader.Load(this);
    }
  }
}
