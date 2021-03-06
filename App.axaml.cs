using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using BugFablesSaveEditor.ViewModels;
using BugFablesSaveEditor.Views;
using ReactiveUI;
using System.Reactive;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;

namespace BugFablesSaveEditor
{
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
        desktop.MainWindow = new MainWindow
        {
          DataContext = new MainWindowViewModel(),
        };
      }

      base.OnFrameworkInitializationCompleted();

      RxApp.DefaultExceptionHandler = Observer.Create<Exception>((ex) =>
      {
        MessageBoxManager.GetMessageBoxStandardWindow("Unexpected error", "An unexpected error occured: " + 
            ex.Message, ButtonEnum.Ok, Icon.Error).ShowDialog(Common.MainWindow);
      });
    }
  }
}
