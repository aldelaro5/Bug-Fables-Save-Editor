using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MessageBox.Avalonia.Enums;
using System;
using System.Diagnostics;
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

      SettingsManager.Load();

      if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
      {
        ExtendClientAreaToDecorationsHint = true;
        ExtendClientAreaChromeHints = Avalonia.Platform.ExtendClientAreaChromeHints.NoChrome;
        ExtendClientAreaTitleBarHeightHint = -1;

        var windowsTitleBar = this.FindControl<WindowsTitleBar>("windowsTitleBar");
        windowsTitleBar.IsVisible = true;
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
      await view.ShowDialog(Common.MainWindow);
    }

    public async void OnLoaded(object sender, EventArgs e)
    {
      if (SettingsManager.Settings.ShowStartupWarning)
      {
        var msg = Common.GetMessageBox("Warning", "This save editor is in beta and thus, " +
                  "may lead to data losses due to instability.\n" +
                  "It is HIGHLY recommended to backup your save files before usage.",
                  ButtonEnum.Ok, MessageBox.Avalonia.Enums.Icon.Warning);
        await msg.ShowDialog(Common.MainWindow);

        SettingsManager.Settings.ShowStartupWarning = false;
        SettingsManager.Save();
      }
    }
  }
}