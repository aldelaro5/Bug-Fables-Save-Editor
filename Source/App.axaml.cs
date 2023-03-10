using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using BugFablesSaveEditor.BugFablesSave;
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
      // TODO: Uncomment this whenever we upgrade to 11 preview 6 or later
      // Line below is needed to remove Avalonia data validation.
      // Without this line you will get duplicate validations from both Avalonia and CT
      // BindingPlugins.DataValidators.RemoveAt(0);
      desktop.MainWindow = new MainWindow { DataContext = new MainWindowViewModel(new SaveData()) };
    }

    base.OnFrameworkInitializationCompleted();
    Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
    Trace.AutoFlush = true;
  }
}
