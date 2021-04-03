using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Common.MessageBox.Enums;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

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

      SettingsManager.Load();

      if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
      {
        ExtendClientAreaToDecorationsHint = true;
        ExtendClientAreaChromeHints = Avalonia.Platform.ExtendClientAreaChromeHints.NoChrome;
        ExtendClientAreaTitleBarHeightHint = -1;

        var windowsTitleBar = this.FindControl<WindowsTitleBar>("windowsTitleBar");
        windowsTitleBar.IsVisible = true;
        TextBlock systemChromeTitle = windowsTitleBar.FindControl<TextBlock>("SystemChromeTitle");
        systemChromeTitle.Text = "Bug Fables Save Editor";
      }

      Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
      Trace.AutoFlush = true;
    }

    private void InitializeComponent()
    {
      AvaloniaXamlLoader.Load(this);
    }

    public async void OnAbout_Click(object sender, RoutedEventArgs e)
    {
      AboutView view = new AboutView();
      view.Width = 500;
      view.Height = 350;
      await view.ShowDialog(CommonUtils.MainWindow);
    }

    public async void OnLoaded(object sender, EventArgs e)
    {
      await Task.Delay(500);
      if (SettingsManager.Settings.ShowStartupWarning)
      {
        var msg = CommonUtils.GetMessageBox("Warning", "This save editor is in beta and thus, " +
                  "may lead to data losses due to instability.\n" +
                  "It is HIGHLY recommended to backup your save files before usage.",
                  ButtonEnum.Ok, Common.MessageBox.Enums.Icon.Warning);
        await msg.ShowDialog(CommonUtils.MainWindow);

        SettingsManager.Settings.ShowStartupWarning = false;
        SettingsManager.Save();
      }
    }
  }
}
