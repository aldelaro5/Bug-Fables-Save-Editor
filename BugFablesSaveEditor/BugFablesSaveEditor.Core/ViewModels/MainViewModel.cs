using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using BugFablesLib;
using BugFablesLib.Data;
using BugFablesLib.SaveData;
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
      bool resumeLoad = await CheckAndFixSaveFile(save);
      if (resumeLoad)
      {
        SaveData.Dispose();
        SaveData = new SaveDataViewModel(save, false);
        SaveInUse = true;
        EditingXboxSave = fileFormat is BfXboxPcSaveDataFormat;
        _fileSaved = true;
      }
      else
      {
        SaveData.Dispose();
        SaveData = new(new BfSaveData(), true);
        CurrentFilePath = "save0.dat";
        SaveInUse = true;
        EditingXboxSave = false;
        _fileSaved = false;
      }
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

  private async Task<bool> CheckAndFixSaveFile(BfSaveData save)
  {
    bool upgradedTo12x = save.Flags[715].Enabled;
    bool mysteryEnabled = save.Flags[681].Enabled;
    bool completedCavesOfTrials = save.Flags[673].Enabled;
    string mysteryMedalsList = save.Flagstrings[13].Str;

    // We do nothing if the save isn't affected by the bricking issue
    if (upgradedTo12x || !mysteryEnabled || (!string.IsNullOrWhiteSpace(mysteryMedalsList) && !completedCavesOfTrials))
      return true;

    var result = await Utils.PlatformSpecifics.ShowMessageBoxAsync(new()
    {
      ContentTitle = "Your save is affected by a 1.2.0 issue",
      ContentMessage = "Your save file is affected by an issue in the 1.2.0 version of the game where the file won't be\n" +
                       "upgraded properly. The save may not be playable and if it is playable, it may be left in an\n" +
                       "inconsistent state. The save editor can automatically fix the save file for you in the meantime\n" +
                       "before a proper game fix is published. This will NOT overwrite your save file, you will need to\n" +
                       "use the \"Save file\" option to save the file once this procedure is complete.\n\n" +
                       "Do you want to proceed with the fixing process?",
      Icon = Icon.Question,
      ButtonDefinitions = ButtonEnum.YesNo
    });

    if (result == ButtonResult.No)
      return true;

    try
    {
      // Add the new starting Merab medals
      save.MedalShopsPools.Merab.Add(new BfMedal { Id = 86 });
      save.MedalShopsPools.Merab.Add(new BfMedal { Id = 84 });
      save.MedalShopsPools.Merab.Add(new BfMedal { Id = 87 });
      save.MedalShopsPools.Merab.Add(new BfMedal { Id = 88 });
      save.MedalShopsPools.Merab.Add(new BfMedal { Id = 81 });

      // Update the mystery pool and shuffle them (the pool will be commited to the flagstring once we are done with it)
      List<int> medals = save.Flagstrings[13].Str
        .Split(',')
        .Where(x => !string.IsNullOrWhiteSpace(x))
        .Select(int.Parse)
        .ToList();
      medals.AddRange([80, 81, 82, 83, 84, 85, 86, 86, 87, 88, 89, 90]);
      RandomSort(ref medals);

      // Give a mystery medal if Cave of Trials was already completed
      if (save.Flags[673].Enabled)
      {
        int medalToGive = medals[0];
        medals.RemoveAt(0);

        save.Medals.Add(new BfMedalOnHandSaveData
          {
            MedalEquipTarget = -2,
            Medal = new BfMedal { Id = medalToGive }
          });
      }

      // We are done messing with the mystery pool so commit it to the flagstring
      save.Flagstrings[13].Str = string.Join(',', medals);

      // Add the second Ambusher to Merab if Chapter 6 was reached already
      if (save.Flags[347].Enabled)
      {
        save.MedalShopsPools.Merab.Add(new BfMedal { Id = 86 });
      }

      // Update Merab shop with shuffle
      save.MedalShopsAvailables.Merab.Clear();
      List<int> merabMedals = save.MedalShopsPools.Merab.Select(m => m.Id).ToList();
      RandomSort(ref merabMedals);
      foreach (int i in merabMedals)
      {
        save.MedalShopsAvailables.Merab.Add(new BfMedal { Id = i });
      }

      // Update Shades shop with shuffle
      save.MedalShopsAvailables.Shades.Clear();
      List<int> shadesMedals = save.MedalShopsPools.Shades.Select(m => m.Id).ToList();
      RandomSort(ref shadesMedals);
      foreach (int i in shadesMedals)
      {
        save.MedalShopsAvailables.Shades.Add(new BfMedal { Id = i });
      }

      // Finally, finalise everything by setting the 1.2.0 upgrade flag
      save.Flags[715].Enabled = true;
    }
    catch (Exception e)
    {
      await Utils.PlatformSpecifics.ShowMessageBoxAsync(new()
      {
        ContentTitle = "Upgrade failed",
        ContentMessage = $"The upgrade process failed with this error: {e.Message}\n\n" +
                         $"The editor will now unload your save file to preserve its integrity and revert to new file creation. " +
                         $"Your original save file was NOT altered.",
        Icon = Icon.Error,
        ButtonDefinitions = ButtonEnum.Ok
      });

      return false;
    }

    await Utils.PlatformSpecifics.ShowMessageBoxAsync(new()
    {
      ContentTitle = "Upgrade successful",
      ContentMessage = "The upgrade process completed successfully. To use your fixed save file, you must use the " +
                       "\"Save file\" option and let the game load the file.",
      Icon = Icon.Success,
      ButtonDefinitions = ButtonEnum.Ok
    });

    return true;
  }

  // The in game shuffle method since Random.Shuffle can't be used on .net 7
  private static void RandomSort(ref List<int> list)
  {
    int[] array = list.ToArray();
    for (int i = 0; i < array.Length; i++)
    {
      int elementToSwap = array[i];
      int randomSwapIndex = Random.Shared.Next(i, array.Length);
      array[i] = array[randomSwapIndex];
      array[randomSwapIndex] = elementToSwap;
    }
    list = new List<int>(array);
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
