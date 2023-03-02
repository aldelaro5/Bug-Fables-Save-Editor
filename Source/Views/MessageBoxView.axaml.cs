using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using BugFablesSaveEditor.ViewModels;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using MessageBox.Avalonia.Views;

namespace BugFablesSaveEditor.Views;

public class MessageBoxView : Window
{
  private readonly DockPanel mainContainer;

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
      ExtendClientAreaChromeHints = ExtendClientAreaChromeHints.NoChrome;
      ExtendClientAreaTitleBarHeightHint = -1;

      WindowsTitleBar? windowsTitleBar = this.FindControl<WindowsTitleBar>("windowsTitleBar");
      windowsTitleBar.IsVisible = true;
    }

    mainContainer = this.FindControl<DockPanel>("mainContainer");
    mainContainer.Children.Add((Grid)messageContent.Content);

    MessageBoxViewModel vm = new(
      new MessageBoxStandardParams
      {
        ContentMessage = text, ContentTitle = title, Icon = icon, ButtonDefinitions = buttons
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

  public ButtonResult ButtonResult { get; set; } = ButtonResult.None;

  private void InitializeComponent()
  {
    AvaloniaXamlLoader.Load(this);
  }

  public ButtonResult GetResult()
  {
    return ButtonResult;
  }
}
