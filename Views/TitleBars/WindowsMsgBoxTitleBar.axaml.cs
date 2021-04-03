using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Runtime.InteropServices;

namespace BugFablesSaveEditor.Views
{
  public class WindowsMsgBoxTitleBar : UserControl
  {
    public WindowsMsgBoxTitleBar()
    {
      this.InitializeComponent();

      if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) == false)
        this.IsVisible = false;
    }

    private void InitializeComponent()
    {
      AvaloniaXamlLoader.Load(this);
    }
  }
}
