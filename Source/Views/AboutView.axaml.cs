using System.Diagnostics;
using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace BugFablesSaveEditor.Views;

public class AboutView : Window
{
  private readonly Label lblVersion;

  public AboutView()
  {
    InitializeComponent();
#if DEBUG
    this.AttachDevTools();
#endif

    lblVersion = this.FindControl<Label>("lblVersion");
    lblVersion.Content = Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString(3);

    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    {
      ExtendClientAreaToDecorationsHint = true;
      ExtendClientAreaChromeHints = ExtendClientAreaChromeHints.NoChrome;
      ExtendClientAreaTitleBarHeightHint = -1;

      WindowsTitleBar? windowsTitleBar = this.FindControl<WindowsTitleBar>("windowsTitleBar");
      windowsTitleBar.IsVisible = true;
    }
  }

  private void InitializeComponent()
  {
    AvaloniaXamlLoader.Load(this);
  }

  public void OpenURL(string url)
  {
    ProcessStartInfo processInfo = new()
    {
      FileName = url,
      UseShellExecute = true
    };
    Process.Start(processInfo);
  }

  public void OnWikiLink_Click(object sender, PointerPressedEventArgs e)
  {
    OpenURL(@"https://github.com/aldelaro5/Bug-Fables-Save-Editor/wiki");
  }

  public void OnGitHubLink_Click(object sender, PointerPressedEventArgs e)
  {
    OpenURL(@"https://github.com/aldelaro5/Bug-Fables-Save-Editor");
  }

  public void OnLicenseLink_Click(object sender, PointerPressedEventArgs e)
  {
    OpenURL(@"https://github.com/aldelaro5/Bug-Fables-Save-Editor/blob/main/LICENSE");
  }

  public void OnOkButton_Click(object sender, RoutedEventArgs e)
  {
    Close();
  }
}
