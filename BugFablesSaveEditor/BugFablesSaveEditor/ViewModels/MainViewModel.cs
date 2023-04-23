using System;
using System.Linq;
using System.Text;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using BugFablesLib;
using BugFablesSaveEditor.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MessageBox.Avalonia.Enums;

namespace BugFablesSaveEditor.ViewModels;

public partial class MainViewModel : ObservableObject
{
  private bool _fileSaved;

  private readonly FilePickerFileType _saveFileFilter = new("Bug Fables save (.dat)")
  {
    Patterns = new[] { "*.dat" }
  };

  [ObservableProperty]
  private SaveDataViewModel _saveData;

  [ObservableProperty]
  [NotifyCanExecuteChangedFor(nameof(CmdSaveFileCommand))]
  private bool _saveInUse;

  [ObservableProperty]
  private string _currentFilePath = "No save file, open an existing file or create a new one";

  public MainViewModel() : this(new BfPcSaveData()) { }

  public MainViewModel(BfPcSaveData saveData) => _saveData = new SaveDataViewModel(saveData, true);

  [RelayCommand(CanExecute = nameof(CanSaveFile))]
  private async void CmdSaveFile()
  {
    var result = await Utils.PlatformSpecifics.SaveFileAsync(SaveData.SaveData, CurrentFilePath);
    if (!result.Succeeded)
      return;

    if (result.NewFilePath is not null)
      CurrentFilePath = result.NewFilePath;
    SaveInUse = true;
    _fileSaved = true;
  }

  private bool CanSaveFile() => SaveInUse;

  [RelayCommand]
  private async void NewFile()
  {
    if (SaveInUse && !_fileSaved)
    {
      var result = await Utils.PlatformSpecifics.ShowMessageBoxAsync(new()
      {
        ContentTitle = "File in use",
        ContentMessage = "An unsaved file is currently in use, creating a new file will loose all unsaved changes,\n" +
                         "are you sure you want to proceed?",
        Icon = Icon.Question,
        ButtonDefinitions = ButtonEnum.YesNo
      });

      if (result == ButtonResult.No)
        return;
    }

    SaveData.Dispose();
    SaveData = new(new BfPcSaveData(), true);
    CurrentFilePath = "save0.dat";
    SaveInUse = true;
    _fileSaved = false;
  }

  [RelayCommand]
  private async void OpenFile()
  {
    if (SaveInUse && !_fileSaved)
    {
      var result = await Utils.PlatformSpecifics.ShowMessageBoxAsync(new()
      {
        ContentTitle = "File in use",
        ContentMessage = "An unsaved file is currently in use, opening a file will loose all unsaved changes,\n" +
                         "are you sure you want to proceed?",
        Icon = Icon.Question,
        ButtonDefinitions = ButtonEnum.YesNo
      });

      if (result == ButtonResult.No)
        return;
    }

    FilePickerOpenOptions pickerOpenOptions = new()
    {
      Title = "Select a Bug Fables save file",
      AllowMultiple = false,
      FileTypeFilter = new[] { _saveFileFilter }
    };
    var files =
      await Utils.TopLevel.StorageProvider.OpenFilePickerAsync(pickerOpenOptions);
    if (files.Count == 0)
      return;

    try
    {
      var fileStream = await files[0].OpenReadAsync();
      byte[] buffer = new byte[fileStream.Length];
      int bytesRead = await fileStream.ReadAsync(buffer);
      if (bytesRead != fileStream.Length || string.IsNullOrEmpty(files[0].Name))
        return;

      string data = Encoding.UTF8.GetString(buffer);
      CurrentFilePath = files[0].Name;
      fileStream.Close();
      var save = new BfPcSaveData();
      save.LoadFromString(data);
      SaveData.Dispose();
      SaveData = new SaveDataViewModel(save, false);
      SaveInUse = true;
      _fileSaved = true;
    }
    catch (Exception ex)
    {
      await Utils.PlatformSpecifics.ShowMessageBoxAsync(new()
      {
        ContentTitle = "Error opening save file",
        ContentMessage = $"An error occured while opening the save file: {ex.Message}",
        Icon = Icon.Error,
        ButtonDefinitions = ButtonEnum.Ok
      });
    }
    finally
    {
      files.First().Dispose();
    }
  }

  [RelayCommand(CanExecute = nameof(CanExit))]
  private void Exit() => ((MainWindow)Utils.TopLevel).Close();

  private bool CanExit() => Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime;
}
