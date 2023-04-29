using System.Diagnostics;
using System.Reflection;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using DialogHostAvalonia;

namespace BugFablesSaveEditor.Core.Views;

public partial class AboutView : UserControl
{
  public AboutView()
  {
    InitializeComponent();

    LblVersion.Content = Assembly.GetExecutingAssembly().GetName().Version?.ToString(3);
  }

  public void OpenUrl(string url)
  {
    ProcessStartInfo processInfo = new() { FileName = url, UseShellExecute = true };
    Process.Start(processInfo);
  }

  public void OnGitHubLink_Click(object sender, PointerPressedEventArgs e)
  {
    OpenUrl(@"https://github.com/aldelaro5/Bug-Fables-Save-Editor");
  }

  public void OnLicenseLink_Click(object sender, PointerPressedEventArgs e)
  {
    OpenUrl(@"https://github.com/aldelaro5/Bug-Fables-Save-Editor/blob/main/LICENSE");
  }

  public void OnOkButton_Click(object sender, RoutedEventArgs e)
  {
    DialogHost.Close(Utils.DialogSessionName);
  }
}
