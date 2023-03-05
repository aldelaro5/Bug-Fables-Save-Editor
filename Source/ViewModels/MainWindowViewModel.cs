using System;
using System.Linq;
using Avalonia.Controls;
using BugFablesSaveEditor.BugFablesSave;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;

namespace BugFablesSaveEditor.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
  [ObservableProperty]
  private CrystalBerriesViewModel _crystalBerriesViewModel;

  [ObservableProperty]
  private string _currentFilePath = "No save file, open an existing file or create a new one";

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
  private SaveData _saveData;

  [ObservableProperty]
  private bool _saveInUse;

  [ObservableProperty]
  private SongsViewModel _songsViewModel;

  [ObservableProperty]
  private StatsViewModel _statsViewModel;
  private bool fileSaved;

  public MainWindowViewModel()
  {
    SaveData saveData = new();
    SaveData = saveData;
    Initialise(saveData);
  }

  public MainWindowViewModel(SaveData saveData)
  {
    SaveData = saveData;
    Initialise(saveData);
  }

  [RelayCommand(CanExecute = nameof(CanSaveFile))]
  private async void CmdSaveFile()
  {
    SaveFileDialog dlg = new();
    dlg.Title = "Select the location to save the file";
    dlg.Filters.Add(new FileDialogFilter { Name = "Bug Fables save (.dat)", Extensions = { "dat" } });
    dlg.DefaultExtension = "dat";
    string filePath = await dlg.ShowAsync(Common.MainWindow);
    if (!string.IsNullOrEmpty(filePath))
    {
      try
      {
        SaveData.SaveToFile(filePath);
        CurrentFilePath = filePath;
        var msg = MessageBoxManager.GetMessageBoxStandardWindow("File saved",
          "The file was saved successfully at " + CurrentFilePath,
          ButtonEnum.Ok, Icon.Warning);
        await msg.ShowDialog(Common.MainWindow);
        fileSaved = true;
      }
      catch (Exception ex)
      {
        var msg = MessageBoxManager.GetMessageBoxStandardWindow("Error opening save file",
          "An error occured while saving the save file: " + ex.Message, ButtonEnum.Ok, Icon.Error);
        await msg.ShowDialog(Common.MainWindow);
      }
    }
  }

  private bool CanSaveFile()
  {
    return SaveInUse;
  }

  [RelayCommand]
  private async void NewFile()
  {
    if (SaveInUse && !fileSaved)
    {
      var msg = MessageBoxManager.GetMessageBoxStandardWindow("File in use", "An unsaed file is currently in use, " +
                                                                             "creating a new file will loose all unsaved changes,\nare you sure you want to proceed?",
        ButtonEnum.YesNo, Icon.Warning);
      var result = await msg.ShowDialog(Common.MainWindow);
      if (result == ButtonResult.No)
      {
        return;
      }
    }

    SaveData.ResetToDefault();
    CurrentFilePath = "New file being created, save it to store it";
    SaveInUse = true;
    fileSaved = false;
  }

  [RelayCommand]
  private async void OpenFile()
  {
    if (SaveInUse && !fileSaved)
    {
      var msg = MessageBoxManager.GetMessageBoxStandardWindow("File in use", "An unsaed file is currently in use, " +
                                                                             "opening a file will loose all unsaved changes,\nare you sure you want to proceed?",
        ButtonEnum.YesNo, Icon.Warning);
      var reuslt = await msg.ShowDialog(Common.MainWindow);
      if (reuslt == ButtonResult.No)
      {
        return;
      }
    }

    OpenFileDialog dlg = new();
    dlg.Title = "Select a Bug Fables save file";
    dlg.Filters.Add(new FileDialogFilter { Name = "Bug Fables save (.dat)", Extensions = { "dat" } });
    dlg.AllowMultiple = false;
    string[] filePaths = await dlg.ShowAsync(Common.MainWindow);
    if (filePaths.Length == 1)
    {
      try
      {
        SaveData.ResetToDefault();
        SaveData.LoadFromFile(filePaths.First());
        CurrentFilePath = filePaths.First();
        SaveInUse = true;
        fileSaved = true;
      }
      catch (Exception ex)
      {
        SaveData.ResetToDefault();
        var msg = MessageBoxManager.GetMessageBoxStandardWindow("Error opening save file",
          "An error occured while opening the save file: " + ex.Message, ButtonEnum.Ok, Icon.Error);
        await msg.ShowDialog(Common.MainWindow);
      }
    }
  }

  [RelayCommand]
  private void Exit()
  {
    Common.MainWindow.Close();
  }

  private void Initialise(SaveData saveData)
  {
    GlobalViewModel = new GlobalViewModel(saveData);
    PartyViewModel = new PartyViewModel(saveData);
    StatsViewModel = new StatsViewModel(saveData);
    QuestsViewModel = new QuestsViewModel(saveData);
    ItemsViewModel = new ItemsViewModel(saveData);
    MedalsViewModel = new MedalsViewModel(saveData);
    LibraryViewModel = new LibraryViewModel(saveData);
    FlagsViewModel = new FlagsViewModel(saveData);
    SongsViewModel = new SongsViewModel(saveData);
    CrystalBerriesViewModel = new CrystalBerriesViewModel(saveData);
  }
}
