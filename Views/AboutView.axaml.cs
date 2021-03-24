using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace BugFablesSaveEditor.Views
{
  public class AboutView : Window
  {
    private Label lblVersion;

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

    public void OpenURL(string url)
    {
      if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
      {
        //https://stackoverflow.com/a/2796367/241446
        using (Process proc = new Process { StartInfo = { UseShellExecute = true, FileName = url } })
        {
          proc.Start();
        }
      }
      else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
      {
        Process.Start("x-www-browser", url);
      }
      else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
      {
        Process.Start("open", url);
      }
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
}
