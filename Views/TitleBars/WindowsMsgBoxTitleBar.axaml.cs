using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Runtime.InteropServices;

namespace BugFablesSaveEditor.Views
{
  public class WindowsMsgBoxTitleBar : UserControl
  {
    private Image windowIcon;

    private DockPanel titleBar;
    private DockPanel titleBarBackground;
    private TextBlock systemChromeTitle;
    private NativeMenuBar seamlessMenuBar;
    private NativeMenuBar defaultMenuBar;

    public WindowsMsgBoxTitleBar()
    {
      this.InitializeComponent();

      if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) == false)
      {
        this.IsVisible = false;
      }
      else
      {
        windowIcon = this.FindControl<Image>("WindowIcon");

        titleBar = this.FindControl<DockPanel>("TitleBar");
        titleBarBackground = this.FindControl<DockPanel>("TitleBarBackground");
        systemChromeTitle = this.FindControl<TextBlock>("SystemChromeTitle");
        seamlessMenuBar = this.FindControl<NativeMenuBar>("SeamlessMenuBar");
        defaultMenuBar = this.FindControl<NativeMenuBar>("DefaultMenuBar");
      }
    }

    private void InitializeComponent()
    {
      AvaloniaXamlLoader.Load(this);
    }
  }
}
