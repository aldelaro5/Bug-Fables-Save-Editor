using System;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace BugFablesSaveEditor.Views;

public partial class MainWindow : Window
{
  public MainWindow()
  {
    InitializeComponent();

    Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
    Trace.AutoFlush = true;
  }

  public async void OnAbout_Click(object sender, RoutedEventArgs e)
  {
    AboutView view = new() { Width = 500, Height = 350 };
    await view.ShowDialog(Utils.MainWindow);
  }
}
