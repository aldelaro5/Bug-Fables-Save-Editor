using System;
using System.Linq;
using BugFablesLib.SaveData;
using BugFablesSaveEditor.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using DynamicData.Binding;
using Humanizer;

namespace BugFablesSaveEditor.ViewModels;

public partial class GlobalViewModel : ObservableObject, IDisposable
{
  private readonly GlobalSaveData _globalSaveData;
  private readonly HeaderSaveData _headerSaveData;
  private readonly IDisposable _areaChangeDispose;

  [ObservableProperty]
  private BfNamedIdModel _currentArea;

  [ObservableProperty]
  private BfNamedIdModel _currentMap;

  public string[] SaveProgressIconNames =>
    Enum.GetNames(typeof(GlobalSaveData.SaveProgressIcon)).Select(x => x.Humanize(LetterCasing.Title)).ToArray();

  public int BerryCount
  {
    get => _globalSaveData.BerryCount;
    set => SetProperty(_globalSaveData.BerryCount, value, _globalSaveData, (data, s) => data.BerryCount = s);
  }

  public int Exp
  {
    get => _globalSaveData.Exp;
    set => SetProperty(_globalSaveData.Exp, value, _globalSaveData, (data, s) => data.Exp = s);
  }

  public int NbrMaxItemsInventory
  {
    get => _globalSaveData.NbrMaxItemsInventory;
    set => SetProperty(_globalSaveData.NbrMaxItemsInventory, value, _globalSaveData,
      (data, s) => data.NbrMaxItemsInventory = s);
  }

  public int NbrMaxItemsStorage
  {
    get => _globalSaveData.NbrMaxItemsStorage;
    set => SetProperty(_globalSaveData.NbrMaxItemsStorage, value, _globalSaveData,
      (data, s) => data.NbrMaxItemsStorage = s);
  }

  public int NeededExp
  {
    get => _globalSaveData.NeededExp;
    set => SetProperty(_globalSaveData.NeededExp, value, _globalSaveData, (data, s) => data.NeededExp = s);
  }

  public int PlayTimeHours
  {
    get => _globalSaveData.PlayTimeHours;
    set => SetProperty(_globalSaveData.PlayTimeHours, value, _globalSaveData, (data, s) => data.PlayTimeHours = s);
  }

  public int PlayTimeMinutes
  {
    get => _globalSaveData.PlayTimeMinutes;
    set => SetProperty(_globalSaveData.PlayTimeMinutes, value, _globalSaveData, (data, s) => data.PlayTimeMinutes = s);
  }

  public int PlayTimeSeconds
  {
    get => _globalSaveData.PlayTimeSeconds;
    set => SetProperty(_globalSaveData.PlayTimeSeconds, value, _globalSaveData, (data, s) => data.PlayTimeSeconds = s);
  }

  public int Rank
  {
    get => _globalSaveData.Rank;
    set => SetProperty(_globalSaveData.Rank, value, _globalSaveData, (data, s) => data.Rank = s);
  }

  public GlobalSaveData.SaveProgressIcon LastProgressIcon
  {
    get => _globalSaveData.LastProgressIcon;
    set => SetProperty(_globalSaveData.LastProgressIcon, value, _globalSaveData,
      (data, s) => data.LastProgressIcon = s);
  }

  public string Filename
  {
    get => _headerSaveData.FileName;
    set => SetProperty(_headerSaveData.FileName, value, _headerSaveData, (data, s) => data.FileName = s);
  }

  public bool IsFrameone
  {
    get => _headerSaveData.IsFrameone;
    set => SetProperty(_headerSaveData.IsFrameone, value, _headerSaveData, (data, s) => data.IsFrameone = s);
  }

  public bool IsHardest
  {
    get => _headerSaveData.IsHardest;
    set => SetProperty(_headerSaveData.IsHardest, value, _headerSaveData, (data, s) => data.IsHardest = s);
  }

  public bool IsMorefarm
  {
    get => _headerSaveData.IsMorefarm;
    set => SetProperty(_headerSaveData.IsMorefarm, value, _headerSaveData, (data, s) => data.IsMorefarm = s);
  }

  public bool IsMystery
  {
    get => _headerSaveData.IsMystery;
    set => SetProperty(_headerSaveData.IsMystery, value, _headerSaveData, (data, s) => data.IsMystery = s);
  }

  public bool IsPushrock
  {
    get => _headerSaveData.IsPushrock;
    set => SetProperty(_headerSaveData.IsPushrock, value, _headerSaveData, (data, s) => data.IsPushrock = s);
  }

  public bool IsRuigee
  {
    get => _headerSaveData.IsRuigee;
    set => SetProperty(_headerSaveData.IsRuigee, value, _headerSaveData, (data, s) => data.IsRuigee = s);
  }

  public float PositionX
  {
    get => _headerSaveData.PositionX;
    set => SetProperty(_headerSaveData.PositionX, value, _headerSaveData, (data, s) => data.PositionX = s);
  }

  public float PositionY
  {
    get => _headerSaveData.PositionY;
    set => SetProperty(_headerSaveData.PositionY, value, _headerSaveData, (data, s) => data.PositionY = s);
  }

  public float PositionZ
  {
    get => _headerSaveData.PositionZ;
    set => SetProperty(_headerSaveData.PositionZ, value, _headerSaveData, (data, s) => data.PositionZ = s);
  }

  public GlobalViewModel() : this(new(), new()) { }

  public GlobalViewModel(GlobalSaveData globalSaveData,
                         HeaderSaveData headerSaveData)
  {
    _globalSaveData = globalSaveData;
    _headerSaveData = headerSaveData;
    _currentMap = new BfNamedIdModel(globalSaveData.CurrentMap);
    _currentArea = new BfNamedIdModel(globalSaveData.CurrentArea);
    _areaChangeDispose = _currentArea
      .WhenValueChanged(x => x.Id)
      .Subscribe(_ => WeakReferenceMessenger.Default.Send(new ValueChangedMessage<BfNamedIdModel>(CurrentArea)));
  }

  public void Dispose()
  {
    _areaChangeDispose.Dispose();
  }
}
