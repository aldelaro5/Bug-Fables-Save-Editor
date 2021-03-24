using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BugFablesSaveEditor.ViewModels;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using MessageBox.Avalonia.Views;
using System.Runtime.InteropServices;

namespace BugFablesSaveEditor.Views
{
  public class MessageBoxView : Window
  {
    private DockPanel mainContainer;
    public ButtonResult ButtonResult { get; set; } = ButtonResult.None;

    public MessageBoxView()
    {
      InitializeComponent();
#if DEBUG
      this.AttachDevTools();
#endif
    }

    public MessageBoxView(string title, string text, ButtonEnum buttons, Icon icon, MsBoxStandardWindow messageContent)
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

      mainContainer = this.FindControl<DockPanel>("mainContainer");
      mainContainer.Children.Add((Grid)messageContent.Content);

      var vm = new MessageBoxViewModel(new MessageBoxStandardParams
      {
        ContentMessage = text,
        ContentTitle = title,
        Icon = icon,
        ButtonDefinitions = buttons
      }, this);
      ((IControl)messageContent).DataContext = vm;

      SizeToContent = messageContent.SizeToContent;
      MinWidth = messageContent.MinWidth;
      MaxWidth = messageContent.MaxWidth;
      Icon = messageContent.Icon;
      MinHeight = messageContent.MinHeight;
      CanResize = messageContent.CanResize;
      FontFamily = messageContent.FontFamily;
      Title = messageContent.Title;
      KeyBindings.AddRange(messageContent.KeyBindings);
    }

    private void InitializeComponent()
    {
      AvaloniaXamlLoader.Load(this);
    }

    public ButtonResult GetResult() => ButtonResult;
  }
}
