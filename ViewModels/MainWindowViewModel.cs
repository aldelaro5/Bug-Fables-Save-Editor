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
    private string _currentFilePath = "No save file, open an existing file or create a new one";
    public string CurrentFilePath
    {
      get { return _currentFilePath; }
      set { _currentFilePath = value; this.RaisePropertyChanged(); }
    }

    public SaveData SaveData
    {
      get
      {
        return Common.saveData;
      }
      set
      {
        Common.saveData = value;
        this.RaisePropertyChanged();
        this.RaisePropertyChanged(nameof(SaveInUse));
      }
    }

    public bool SaveInUse
    {
      get { return SaveData != null; }
    }

    public ReactiveCommand<Unit, Unit> CmdNewFile
    {
      get => ReactiveCommand.Create(() =>
      {
        SaveData = new SaveData();
        CurrentFilePath = "New file being created, save it to store it";
      });
    }

    public ReactiveCommand<Unit, Unit> CmdOpenFile
    {
      get => ReactiveCommand.CreateFromTask(async () =>
      {
        OpenFileDialog dlg = new OpenFileDialog();
        dlg.Title = "Select a Bug Fables save file";
        dlg.Filters.Add(new FileDialogFilter() { Name = "Bug Fables save (.dat)", Extensions = { "dat" } });
        dlg.AllowMultiple = false;
        string[] filePaths = await dlg.ShowAsync(Common.MainWindow);
        if (filePaths.Length == 1)
        {
          SaveData = new SaveData();
          try
          {
            SaveData.LoadFromFile(filePaths.First());
            CurrentFilePath = filePaths.First();
          }
          catch (Exception ex)
          {
            SaveData = null;
            var msg = MessageBoxManager.GetMessageBoxStandardWindow("Error opening save file",
                        "An error occured while opening the save file: " + ex.Message, ButtonEnum.Ok, Icon.Error);
            await msg.ShowDialog(Common.MainWindow);
          }
        }
      });
    }

    public ReactiveCommand<Unit, Unit> CmdSaveFile
    {
      get => ReactiveCommand.CreateFromTask(async () =>
      {
        SaveFileDialog dlg = new SaveFileDialog();
        dlg.Title = "Select the location to save the file";
        dlg.Filters.Add(new FileDialogFilter() { Name = "Bug Fables save (.dat)", Extensions = { "dat" } });
        dlg.DefaultExtension = "dat";
        string filePath = await dlg.ShowAsync(Common.MainWindow);
        if (!string.IsNullOrEmpty(filePath))
        {
          try
          {
            SaveData.SaveToFile(filePath);
            CurrentFilePath = filePath;
          }
          catch (Exception ex)
          {
            var msg = MessageBoxManager.GetMessageBoxStandardWindow("Error opening save file",
                        "An error occured while saving the save file: " + ex.Message, ButtonEnum.Ok, Icon.Error);
            await msg.ShowDialog(Common.MainWindow);
          }
        }
      }, this.WhenAnyValue(x => x.SaveInUse));
    }

    public ReactiveCommand<Unit, Unit> CmdExit
    {
      get => ReactiveCommand.Create(() =>
      {
        Common.MainWindow.Close();
      });
    }

    public MainWindowViewModel()
    {
    }
  }
}
