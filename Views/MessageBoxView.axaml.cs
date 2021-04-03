using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BugFablesSaveEditor.ViewModels;
using Common.MessageBox;
using Common.MessageBox.Enums;
using System.Runtime.InteropServices;

namespace BugFablesSaveEditor.Views
{
  public class MessageBoxView : Window
  {
    public ButtonResult ButtonResult { get; set; } = ButtonResult.None;

    public MessageBoxView()
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

        var windowsTitleBar = this.FindControl<WindowsMsgBoxTitleBar>("windowsTitleBar");
        windowsTitleBar.IsVisible = true;
      }

      var vm = new MessageBoxViewModel(new MessageBoxParams
      {
        ContentMessage = "This is a test message box message that can go for long and long, " +
                         "please read this message to understand what is happening in this message box." +
                         "The brown fox jumps over the lazy dog",
        ContentTitle = "Test nessage box",
        Icon = Common.MessageBox.Enums.Icon.Warning,
        WindowStartupLocation = WindowStartupLocation.CenterScreen,
        MaxWidth = 600,
        ButtonDefinitions = ButtonEnum.Ok
      }, this);
      DataContext = vm;
    }

    public MessageBoxView(string title, string text, ButtonEnum buttons, Icon icon)
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

        var windowsTitleBar = this.FindControl<WindowsMsgBoxTitleBar>("windowsTitleBar");
        windowsTitleBar.IsVisible = true;
      }

      var vm = new MessageBoxViewModel(new MessageBoxParams
      {
        ContentMessage = text,
        ContentTitle = title,
        Icon = icon,
        WindowStartupLocation = WindowStartupLocation.CenterScreen,
        MaxWidth = 600,
        ButtonDefinitions = buttons
      }, this);
      DataContext = vm;
      // Workaround a layout issue
      Width = 600;
    }

    private void InitializeComponent()
    {
      AvaloniaXamlLoader.Load(this);
    }

    public ButtonResult GetResult() => ButtonResult;
  }
}
