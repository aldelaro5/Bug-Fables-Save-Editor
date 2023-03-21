using System;
using System.Linq;
using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;
using Humanizer;
using static BugFablesLib.SaveData.GlobalSaveData;

namespace BugFablesSaveEditor.ViewModels;

[ObservableObject]
public partial class ObservableGlobalSaveData : BfObservable
{
  private readonly GlobalSaveData _globalSaveData;

  [ObservableProperty]
  private ObservableBfResource _currentArea;

  [ObservableProperty]
  private ObservableBfResource _currentMap;

  public string[] SaveProgressIconNames
  {
    get => Enum.GetNames(typeof(SaveProgressIcon)).Select(x => x.Humanize(LetterCasing.Title))
      .ToArray();
  }

  public int BerryCount
  {
    get => _globalSaveData.BerryCount;
    set => SetProperty(_globalSaveData.BerryCount, value, _globalSaveData,
      (x, y) => x.BerryCount = y);
  }

  public int Exp
  {
    get => _globalSaveData.Exp;
    set => SetProperty(_globalSaveData.Exp, value, _globalSaveData,
      (x, y) => x.Exp = y);
  }

  public int MaxMp
  {
    get => _globalSaveData.MaxMp;
    set => SetProperty(_globalSaveData.MaxMp, value, _globalSaveData,
      (x, y) => x.MaxMp = y);
  }

  public int MaxTp
  {
    get => _globalSaveData.MaxTp;
    set => SetProperty(_globalSaveData.MaxTp, value, _globalSaveData,
      (x, y) => x.MaxTp = y);
  }

  public int Mp
  {
    get => _globalSaveData.Mp;
    set => SetProperty(_globalSaveData.Mp, value, _globalSaveData,
      (x, y) => x.Mp = y);
  }

  public int NbrMaxItemsInventory
  {
    get => _globalSaveData.NbrMaxItemsInventory;
    set => SetProperty(_globalSaveData.NbrMaxItemsInventory, value, _globalSaveData,
      (x, y) => x.NbrMaxItemsInventory = y);
  }

  public int NbrMaxItemsStorage
  {
    get => _globalSaveData.NbrMaxItemsStorage;
    set => SetProperty(_globalSaveData.NbrMaxItemsStorage, value, _globalSaveData,
      (x, y) => x.NbrMaxItemsStorage = y);
  }

  public int NeededExp
  {
    get => _globalSaveData.NeededExp;
    set => SetProperty(_globalSaveData.NeededExp, value, _globalSaveData,
      (x, y) => x.NeededExp = y);
  }

  public int PlayTimeHours
  {
    get => _globalSaveData.PlayTimeHours;
    set => SetProperty(_globalSaveData.PlayTimeHours, value, _globalSaveData,
      (x, y) => x.PlayTimeHours = y);
  }

  public int PlayTimeMinutes
  {
    get => _globalSaveData.PlayTimeMinutes;
    set => SetProperty(_globalSaveData.PlayTimeMinutes, value, _globalSaveData,
      (x, y) => x.PlayTimeMinutes = y);
  }

  public int PlayTimeSeconds
  {
    get => _globalSaveData.PlayTimeSeconds;
    set => SetProperty(_globalSaveData.PlayTimeSeconds, value, _globalSaveData,
      (x, y) => x.PlayTimeSeconds = y);
  }

  public int Rank
  {
    get => _globalSaveData.Rank;
    set => SetProperty(_globalSaveData.Rank, value, _globalSaveData,
      (x, y) => x.Rank = y);
  }

  public int LastProgressIcon
  {
    get => (int)_globalSaveData.LastProgressIcon;
    set => SetProperty((int)_globalSaveData.LastProgressIcon, value, _globalSaveData,
      (x, y) => x.LastProgressIcon = (SaveProgressIcon)y);
  }

  public int Tp
  {
    get => _globalSaveData.Tp;
    set => SetProperty(_globalSaveData.Tp, value, _globalSaveData,
      (x, y) => x.Tp = y);
  }

  public ObservableGlobalSaveData(GlobalSaveData globalSaveDatal) :
    base(globalSaveDatal)
  {
    _globalSaveData = globalSaveDatal;
    _currentMap = new ObservableBfResource(_globalSaveData.CurrentMap);
    _currentArea = new ObservableBfResource(_globalSaveData.CurrentArea);
  }
}
