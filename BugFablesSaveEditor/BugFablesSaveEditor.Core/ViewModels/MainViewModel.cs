using System;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using BugFablesLib;
using BugFablesSaveEditor.Core.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DialogHostAvalonia;
using MsBox.Avalonia.Enums;

namespace BugFablesSaveEditor.Core.ViewModels;

public partial class MainViewModel : ObservableObject
{
  private const string DefaultFileMessage = "No save file, open an existing file or create a new one";
  private bool _fileSaved;

  private readonly FilePickerFileType _saveFileFilter = new("Bug Fables save (.dat)")
  {
    Patterns = new[] { "*.dat" }
  };

  public static BfPcSaveDataFormat PcSaveDataFormat => BfSaveData.PcSaveDataFormat;
  public static BfSwitchSaveDataFormat SwitchSaveDataFormat => BfSaveData.SwitchSaveDataFormat;
  public static BfXboxPcSaveDataFormat XboxPcSaveDataFormat { get; } = new(SelectXboxPcSaveFile);

  [ObservableProperty]
  private SaveDataViewModel _saveData;

  [ObservableProperty]
  [NotifyCanExecuteChangedFor(nameof(SaveFileCommand))]
  private bool _saveInUse;

  [ObservableProperty]
  private bool _editingXboxSave;

  [ObservableProperty]
  private string _currentFilePath = DefaultFileMessage;

  public MainViewModel() : this(new()) { }

  public MainViewModel(BfSaveData saveData) => _saveData = new SaveDataViewModel(saveData, true);

  [RelayCommand(CanExecute = nameof(CanSaveFile))]
  private async Task SaveFile(IBfSaveFileFormat fileFormat)
  {
    var result = await Utils.PlatformSpecifics.SaveFileAsync(SaveData.SaveData, fileFormat, CurrentFilePath);
    if (!result.Succeeded)
      return;

    if (result.NewFilePath is not null)
      CurrentFilePath = result.NewFilePath;
    SaveInUse = true;
    _fileSaved = true;
  }

  private bool CanSaveFile() => SaveInUse;

  [RelayCommand]
  private async Task NewFile()
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
    SaveData = new(new BfSaveData(), true);
    CurrentFilePath = "save0.dat";
    SaveInUse = true;
    EditingXboxSave = false;
    _fileSaved = false;
  }

  [RelayCommand]
  private async Task OpenFile(IBfSaveFileFormat fileFormat)
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
      AllowMultiple = false
    };
    if (fileFormat is not BfXboxPcSaveDataFormat)
      pickerOpenOptions.FileTypeFilter = new[] { _saveFileFilter };

    var files = await Utils.TopLevel.StorageProvider.OpenFilePickerAsync(pickerOpenOptions);
    if (files.Count == 0)
      return;

    try
    {
      var fileStream = await files[0].OpenReadAsync();
      byte[] buffer = new byte[fileStream.Length];
      int bytesRead = await fileStream.ReadAsync(buffer);
      if (bytesRead != fileStream.Length || string.IsNullOrEmpty(files[0].Name))
        return;

      CurrentFilePath = files[0].Name;
      fileStream.Close();
      var save = new BfSaveData();
      await save.LoadFromBytes(buffer, fileFormat);
      SaveData.Dispose();
      SaveData = new SaveDataViewModel(save, false);
      SaveInUse = true;
      EditingXboxSave = fileFormat is BfXboxPcSaveDataFormat;
      _fileSaved = true;
    }
    catch (Exception ex)
    {
      CurrentFilePath = DefaultFileMessage;
      SaveInUse = false;
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

  private static async Task<int> SelectXboxPcSaveFile(string[] fileNames)
  {
    XboxFileSelectViewModel viewModel = new(fileNames);
    XboxFileSelectView view = new() { DataContext = viewModel };
    await DialogHost.Show(view, Utils.DialogSessionName);
    return viewModel.ResultIndex;
  }

  [RelayCommand(CanExecute = nameof(CanExit))]
  private void Exit() => ((MainWindow)Utils.TopLevel).Close();

  private bool CanExit() => Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime;
}
