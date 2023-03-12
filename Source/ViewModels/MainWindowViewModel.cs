using System;
using System.Linq;
using Avalonia.Platform.Storage;
using BugFablesSaveEditor.BugFablesSave;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;

namespace BugFablesSaveEditor.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
  private readonly FilePickerFileType _saveFileFilter =
    new("Bug Fables save (.dat)") { Patterns = new[] { "*.dat" } };

  private bool _fileSaved;

  [ObservableProperty]
  private SaveData _saveData;

  [ObservableProperty]
  [NotifyCanExecuteChangedFor(nameof(CmdSaveFileCommand))]
  private bool _saveInUse;

  [ObservableProperty]
  private string _currentFilePath = "No save file, open an existing file or create a new one";

  [ObservableProperty]
  private CrystalBerriesViewModel _crystalBerriesViewModel;

  [ObservableProperty]
  private FlagsViewModel _flagsViewModel;

  [ObservableProperty]
  private GlobalViewModel _globalViewModel;

  [ObservableProperty]
  private ItemsViewModel _itemsViewModel;

  [ObservableProperty]
  private LibraryViewModel _libraryViewModel;

  [ObservableProperty]
  private MedalsViewModel _medalsViewModel;

  [ObservableProperty]
  private PartyViewModel _partyViewModel;

  [ObservableProperty]
  private QuestsViewModel _questsViewModel;

  [ObservableProperty]
  private SongsViewModel _songsViewModel;

  [ObservableProperty]
  private StatsViewModel _statsViewModel;

  public MainWindowViewModel() : this(new SaveData())
  {
  }

  public MainWindowViewModel(SaveData saveData)
  {
    _saveData = saveData;
    _globalViewModel = new(saveData);
    _partyViewModel = new(saveData);
    _statsViewModel = new(saveData);
    _questsViewModel = new(saveData);
    _itemsViewModel = new(saveData);
    _medalsViewModel = new(saveData);
    _libraryViewModel = new(saveData);
    _flagsViewModel = new(saveData);
    _songsViewModel = new(saveData);
    _crystalBerriesViewModel = new(saveData);
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
      if (!file.TryGetUri(out Uri? fileUri))
        return;

      SaveData.SaveToFile(fileUri.LocalPath);
      CurrentFilePath = fileUri.LocalPath;
      await MessageBoxManager.GetMessageBoxStandardWindow("File saved",
        $"The file was saved successfully at {CurrentFilePath}",
        ButtonEnum.Ok, Icon.Warning).ShowDialog(Utils.MainWindow);
      _fileSaved = true;
    }
    catch (Exception ex)
    {
      var msg = MessageBoxManager.GetMessageBoxStandardWindow("Error opening save file",
        $"An error occured while saving the save file: {ex.Message}", ButtonEnum.Ok, Icon.Error);
      await msg.ShowDialog(Utils.MainWindow);
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
  private async void NewFile()
  {
    if (SaveInUse && !_fileSaved)
    {
      var msg = MessageBoxManager.GetMessageBoxStandardWindow("File in use",
        "An unsaved file is currently in use, creating a new file will loose all unsaved changes,\n" +
        "are you sure you want to proceed?",
        ButtonEnum.YesNo, Icon.Warning);
      var result = await msg.ShowDialog(Utils.MainWindow);
      if (result == ButtonResult.No)
        return;
    }

    SaveData.ResetToDefault();
    CurrentFilePath = "New file being created, save it to store it";
    SaveInUse = true;
    _fileSaved = false;
  }

  [RelayCommand]
  private async void OpenFile()
  {
    if (SaveInUse && !_fileSaved)
    {
      var result = await MessageBoxManager.GetMessageBoxStandardWindow("File in use",
        "An unsaved file is currently in use, opening a file will loose all unsaved changes,\n" +
        "are you sure you want to proceed?",
        ButtonEnum.YesNo, Icon.Warning).ShowDialog(Utils.MainWindow);
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
      await Utils.MainWindow.StorageProvider.OpenFilePickerAsync(pickerOpenOptions);
    if (files.Count == 0)
      return;

    try
    {
      if (!files.First().TryGetUri(out Uri? fileUri))
        return;

      SaveData.ResetToDefault();
      SaveData.LoadFromFile(fileUri.LocalPath);
      CurrentFilePath = fileUri.LocalPath;
      SaveInUse = true;
      _fileSaved = true;
    }
    catch (Exception ex)
    {
      SaveData.ResetToDefault();
      var msg = MessageBoxManager.GetMessageBoxStandardWindow("Error opening save file",
        $"An error occured while opening the save file: {ex.Message}", ButtonEnum.Ok, Icon.Error);
      await msg.ShowDialog(Utils.MainWindow);
    }
    finally
    {
      files.First().Dispose();
    }
  }

  [RelayCommand]
  private void Exit() => Utils.MainWindow.Close();
}
