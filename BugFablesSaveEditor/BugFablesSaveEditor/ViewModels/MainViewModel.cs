using System;
using System.IO;
using System.Linq;
using Avalonia.Platform.Storage;
using BugFablesLib;
using BugFablesSaveEditor.ObservableModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BugFablesSaveEditor.ViewModels;

public partial class MainViewModel : ObservableObject
{
  private readonly FilePickerFileType _saveFileFilter =
    new("Bug Fables save (.dat)") { Patterns = new[] { "*.dat" } };

  private bool _fileSaved;

  [ObservableProperty]
  private SaveDataViewModel _saveData;

  [ObservableProperty]
  [NotifyCanExecuteChangedFor(nameof(CmdSaveFileCommand))]
  private bool _saveInUse;

  [ObservableProperty]
  private string _currentFilePath = "No save file, open an existing file or create a new one";

  public MainViewModel() : this(new BfPcSaveData())
  {
  }

  public MainViewModel(BfPcSaveData saveData)
  {
    _saveData = new SaveDataViewModel(saveData);
  }

  [RelayCommand(CanExecute = nameof(CanSaveFile))]
  private async void CmdSaveFile()
  {
    FilePickerSaveOptions pickerSaveOptions = new()
    {
      Title = "Select the location to save the file",
      ShowOverwritePrompt = true,
      FileTypeChoices = new[] { _saveFileFilter }
    };

    var file = await Utils.MainWindow.StorageProvider.SaveFilePickerAsync(pickerSaveOptions);
    if (file is null)
      return;

    try
    {
      string? path = file.TryGetLocalPath();
      if (string.IsNullOrEmpty(path))
        return;

      var data = SaveData._saveData.EncodeToString();
      File.WriteAllText(path, data);
      CurrentFilePath = path;
      // await MessageBoxManager.GetMessageBoxStandardWindow("File saved",
      //   $"The file was saved successfully at {CurrentFilePath}",
      //   ButtonEnum.Ok, Icon.Warning).ShowDialog(Utils.MainWindow);
      // _fileSaved = true;
    }
    catch (Exception /*ex*/)
    {
      // var msg = MessageBoxManager.GetMessageBoxStandardWindow("Error opening save file",
      //   $"An error occured while saving the save file: {ex.Message}", ButtonEnum.Ok, Icon.Error);
      // await msg.ShowDialog(Utils.MainWindow);
    }
    finally
    {
      file.Dispose();
    }
  }

  private bool CanSaveFile()
  {
    return SaveInUse;
  }

  [RelayCommand]
  private /*async*/ void NewFile()
  {
    if (SaveInUse && !_fileSaved)
    {
      // var msg = MessageBoxManager.GetMessageBoxStandardWindow("File in use",
      //   "An unsaved file is currently in use, creating a new file will loose all unsaved changes,\n" +
      //   "are you sure you want to proceed?",
      //   ButtonEnum.YesNo, Icon.Warning);
      // var result = await msg.ShowDialog(Utils.MainWindow);
      // if (result == ButtonResult.No)
      //   return;
    }

    SaveData = new(new BfPcSaveData());
    CurrentFilePath = "New file being created, save it to store it";
    SaveInUse = true;
    _fileSaved = false;
  }

  [RelayCommand]
  private async void OpenFile()
  {
    if (SaveInUse && !_fileSaved)
    {
      // var result = await MessageBoxManager.GetMessageBoxStandardWindow("File in use",
      //   "An unsaved file is currently in use, opening a file will loose all unsaved changes,\n" +
      //   "are you sure you want to proceed?",
      //   ButtonEnum.YesNo, Icon.Warning).ShowDialog(Utils.MainWindow);
      // if (result == ButtonResult.No)
      //   return;
    }

    FilePickerOpenOptions pickerOpenOptions = new()
    {
      Title = "Select a Bug Fables save file",
      AllowMultiple = false,
      FileTypeFilter = new[] { _saveFileFilter }
    };
    var files =
      await Utils.MainWindow.StorageProvider.OpenFilePickerAsync(pickerOpenOptions);
    if (files.Count == 0)
      return;

    try
    {
      string? path = files.First().TryGetLocalPath();
      if (string.IsNullOrEmpty(path))
        return;

      var data = File.ReadAllText(path);
      var save = new BfPcSaveData();
      save.LoadFromString(data);
      SaveData = new SaveDataViewModel(save);
      CurrentFilePath = path;
      SaveInUse = true;
      _fileSaved = true;
    }
    catch (Exception /*ex*/)
    {
      // var msg = MessageBoxManager.GetMessageBoxStandardWindow("Error opening save file",
      //   $"An error occured while opening the save file: {ex.Message}", ButtonEnum.Ok, Icon.Error);
      // await msg.ShowDialog(Utils.MainWindow);
    }
    finally
    {
      files.First().Dispose();
    }
  }

  [RelayCommand]
  private void Exit() => Utils.MainWindow.Close();
}
