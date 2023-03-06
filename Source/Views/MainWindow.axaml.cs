using System;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;
using BugFablesSaveEditor.Utils;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;

namespace BugFablesSaveEditor.Views;

public partial class MainWindow : Window
{
  public MainWindow()
  {
    InitializeComponent();
    
    SettingsManager.Load();

    Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
    Trace.AutoFlush = true;
  }

  public async void OnAbout_Click(object sender, RoutedEventArgs e)
  {
    AboutView view = new() { Width = 500, Height = 350 };
    await view.ShowDialog(Common.MainWindow);
  }

  public async void OnLoaded(object sender, EventArgs e)
  {
    if (SettingsManager.Settings.ShowStartupWarning)
    {
      var msg = MessageBoxManager.GetMessageBoxStandardWindow("Warning",
        "This save editor is in beta and thus, " +
        "may lead to data losses due to instability.\n" +
        "It is HIGHLY recommended to backup your save files before usage.",
        ButtonEnum.Ok, MessageBox.Avalonia.Enums.Icon.Warning);
      await msg.ShowDialog(Common.MainWindow);

      SettingsManager.Settings.ShowStartupWarning = false;
      SettingsManager.Save();
    }
  }
}
