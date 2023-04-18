using System;
using System.Linq;
using System.Text;
using Avalonia.Platform.Storage;
using BugFablesLib;
using BugFablesSaveEditor.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BugFablesSaveEditor.ViewModels;

public partial class MainViewModel : ObservableObject
{
  //private bool _fileSaved;

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
    FilePickerSaveOptions pickerSaveOptions = new()
    {
      Title = "Select the location to save the file",
      ShowOverwritePrompt = true,
      FileTypeChoices = new[] { _saveFileFilter }
    };

    var file = await Utils.TopLevel.StorageProvider.SaveFilePickerAsync(pickerSaveOptions);
    if (file is null)
      return;

    try
    {
      var fileStream = await file.OpenWriteAsync();
      string data = SaveData.SaveData.EncodeToString();
      await fileStream.WriteAsync(Encoding.UTF8.GetBytes(data));
      CurrentFilePath = file.Name;
      fileStream.Close();
      // await MessageBoxManager.GetMessageBoxStandardWindow("File saved",
      //   $"The file was saved successfully at {CurrentFilePath}",
      //   ButtonEnum.Ok, Icon.Warning).ShowDialog(Utils.MainWindow);
      //_fileSaved = true;
    }
    // catch (Exception ex)
    // {
    //   var msg = MessageBoxManager.GetMessageBoxStandardWindow("Error opening save file",
    //     $"An error occured while saving the save file: {ex.Message}", ButtonEnum.Ok, Icon.Error);
    //   await msg.ShowDialog(Utils.MainWindow);
    // }
    finally
    {
      file.Dispose();
    }
  }

  private bool CanSaveFile() => SaveInUse;

  [RelayCommand]
  private void NewFile()
  {
    // if (SaveInUse && !_fileSaved)
    // {
    //   var msg = MessageBoxManager.GetMessageBoxStandardWindow("File in use",
    //     "An unsaved file is currently in use, creating a new file will loose all unsaved changes,\n" +
    //     "are you sure you want to proceed?",
    //     ButtonEnum.YesNo, Icon.Warning);
    //   var result = await msg.ShowDialog(Utils.MainWindow);
    //   if (result == ButtonResult.No)
    //     return;
    // }

    SaveData.Dispose();
    SaveData = new(new BfPcSaveData(), true);
    CurrentFilePath = "New file being created, save it to store it";
    SaveInUse = true;
    //_fileSaved = false;
  }

  [RelayCommand]
  private async void OpenFile()
  {
    // if (SaveInUse && !_fileSaved)
    // {
    //   var result = await MessageBoxManager.GetMessageBoxStandardWindow("File in use",
    //     "An unsaved file is currently in use, opening a file will loose all unsaved changes,\n" +
    //     "are you sure you want to proceed?",
    //     ButtonEnum.YesNo, Icon.Warning).ShowDialog(Utils.MainWindow);
    //   if (result == ButtonResult.No)
    //     return;
    // }

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
      //_fileSaved = true;
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
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
  private void Exit() => ((MainWindow)Utils.TopLevel).Close();
}
