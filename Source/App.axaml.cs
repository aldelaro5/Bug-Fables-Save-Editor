using System;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using BugFablesSaveEditor.BugFablesSave;
using BugFablesSaveEditor.ViewModels;
using BugFablesSaveEditor.Views;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;
using ReactiveUI;

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
      // Line below is needed to remove Avalonia data validation.
      // Without this line you will get duplicate validations from both Avalonia and CT
      BindingPlugins.DataValidators.RemoveAt(0);
      desktop.MainWindow = new MainWindow { DataContext = new MainWindowViewModel(new SaveData()) };
    }

    base.OnFrameworkInitializationCompleted();

    RxApp.DefaultExceptionHandler = Observer.Create<Exception>(ex =>
    {
      var msgBox = MessageBoxManager.GetMessageBoxStandardWindow("Unexpected error", "An unexpected error occured: " +
                                                                              ex.Message, ButtonEnum.Ok, Icon.Error);
      Task.Run(() => msgBox.ShowDialog(Common.MainWindow));
    });
  }
}
