using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using BugFablesSaveEditor.BugFablesSave;
using BugFablesSaveEditor.ViewModels;
using BugFablesSaveEditor.Views;
using Common.MessageBox.Enums;
using ReactiveUI;
using System;
using System.Reactive;

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
          DataContext = new MainWindowViewModel(new SaveData()),
        };
      }

      base.OnFrameworkInitializationCompleted();

      RxApp.DefaultExceptionHandler = Observer.Create<Exception>((ex) =>
      {
        CommonUtils.GetMessageBox("Unexpected error", "An unexpected error occured: " +
            ex.Message, ButtonEnum.Ok, Icon.Error).ShowDialog(CommonUtils.MainWindow);
      });
    }
  }
}
