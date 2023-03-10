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

    Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
    Trace.AutoFlush = true;
  }

  public async void OnAbout_Click(object sender, RoutedEventArgs e)
  {
    AboutView view = new() { Width = 500, Height = 350 };
    await view.ShowDialog(Common.MainWindow);
  }
}
