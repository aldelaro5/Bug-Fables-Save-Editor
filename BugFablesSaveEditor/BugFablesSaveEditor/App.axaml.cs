using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using BugFablesSaveEditor.ViewModels;
using BugFablesSaveEditor.Views;

namespace BugFablesSaveEditor;

public class App : Application
{
  public override void Initialize()
  {
    AvaloniaXamlLoader.Load(this);
  }

  public override void OnFrameworkInitializationCompleted()
  {
    if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
    {
      BindingPlugins.DataValidators.RemoveAt(0);
      desktop.MainWindow = new MainWindow { DataContext = new MainViewModel() };
    }
    else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
    {
      singleViewPlatform.MainView = new MainView { DataContext = new MainViewModel() };
    }

    base.OnFrameworkInitializationCompleted();
    Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
    Trace.AutoFlush = true;
  }
}
