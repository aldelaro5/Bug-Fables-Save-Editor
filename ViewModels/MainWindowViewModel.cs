using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using ReactiveUI;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;
using Avalonia.Controls;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.ObjectModel;
using System.Linq;
using BugFablesSaveEditor.BugFablesSave;
using BugFablesSaveEditor.BugFablesSave.Sections;
using BugFablesSaveEditor.BugFablesEnums;

namespace BugFablesSaveEditor.ViewModels
{
  public class MainWindowViewModel : ViewModelBase
  {
    public ReactiveCommand<Unit, Unit> CmdTest
    {
      get => ReactiveCommand.Create(() =>
      {
        SaveData saveDataFile = new SaveData();
        saveDataFile.LoadFromFile(@"C:\Users\aldel\Documents\save1.dat");
        saveDataFile.SaveToFile(@"C:\Users\aldel\Documents\save1-copy.dat");
        MessageBoxManager.GetMessageBoxStandardWindow("Success", "File written sucessdully", ButtonEnum.Ok, Icon.Warning).ShowDialog(Common.MainWindow);
      });
    }

    public MainWindowViewModel()
    {
    }

  }
}
