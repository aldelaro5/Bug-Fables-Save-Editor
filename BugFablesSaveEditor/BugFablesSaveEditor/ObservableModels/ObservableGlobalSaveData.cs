using System;
using System.Linq;
using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using DynamicData.Binding;
using Humanizer;
using static BugFablesLib.SaveData.GlobalSaveData;

namespace BugFablesSaveEditor.ObservableModels;

public sealed partial class ObservableGlobalSaveData : ObservableModel
{
  public override GlobalSaveData UnderlyingData { get; }

  [ObservableProperty]
  private ObservableBfNamedId _currentArea;

  [ObservableProperty]
  private ObservableBfNamedId _currentMap;

  public string[] SaveProgressIconNames =>
    Enum.GetNames(typeof(SaveProgressIcon)).Select(x => x.Humanize(LetterCasing.Title)).ToArray();

  public int BerryCount
  {
    get => UnderlyingData.BerryCount;
    set => SetProperty(UnderlyingData.BerryCount, value, UnderlyingData, (data, s) => data.BerryCount = s);
  }

  public int Exp
  {
    get => UnderlyingData.Exp;
    set => SetProperty(UnderlyingData.Exp, value, UnderlyingData, (data, s) => data.Exp = s);
  }

  public int MaxMp
  {
    get => UnderlyingData.MaxMp;
    set => SetProperty(UnderlyingData.MaxMp, value, UnderlyingData, (data, s) => data.MaxMp = s);
  }

  public int MaxTp
  {
    get => UnderlyingData.MaxTp;
    set => SetProperty(UnderlyingData.MaxTp, value, UnderlyingData, (data, s) => data.MaxTp = s);
  }

  public int Mp
  {
    get => UnderlyingData.Mp;
    set => SetProperty(UnderlyingData.Mp, value, UnderlyingData, (data, s) => data.Mp = s);
  }

  public int NbrMaxItemsInventory
  {
    get => UnderlyingData.NbrMaxItemsInventory;
    set => SetProperty(UnderlyingData.NbrMaxItemsInventory, value, UnderlyingData,
      (data, s) => data.NbrMaxItemsInventory = s);
  }

  public int NbrMaxItemsStorage
  {
    get => UnderlyingData.NbrMaxItemsStorage;
    set => SetProperty(UnderlyingData.NbrMaxItemsStorage, value, UnderlyingData,
      (data, s) => data.NbrMaxItemsStorage = s);
  }

  public int NeededExp
  {
    get => UnderlyingData.NeededExp;
    set => SetProperty(UnderlyingData.NeededExp, value, UnderlyingData, (data, s) => data.NeededExp = s);
  }

  public int PlayTimeHours
  {
    get => UnderlyingData.PlayTimeHours;
    set => SetProperty(UnderlyingData.PlayTimeHours, value, UnderlyingData, (data, s) => data.PlayTimeHours = s);
  }

  public int PlayTimeMinutes
  {
    get => UnderlyingData.PlayTimeMinutes;
    set => SetProperty(UnderlyingData.PlayTimeMinutes, value, UnderlyingData, (data, s) => data.PlayTimeMinutes = s);
  }

  public int PlayTimeSeconds
  {
    get => UnderlyingData.PlayTimeSeconds;
    set => SetProperty(UnderlyingData.PlayTimeSeconds, value, UnderlyingData, (data, s) => data.PlayTimeSeconds = s);
  }

  public int Rank
  {
    get => UnderlyingData.Rank;
    set => SetProperty(UnderlyingData.Rank, value, UnderlyingData, (data, s) => data.Rank = s);
  }

  public SaveProgressIcon LastProgressIcon
  {
    get => UnderlyingData.LastProgressIcon;
    set => SetProperty(UnderlyingData.LastProgressIcon, value, UnderlyingData, (data, s) => data.LastProgressIcon = s);
  }

  public int Tp
  {
    get => UnderlyingData.Tp;
    set => SetProperty(UnderlyingData.Tp, value, UnderlyingData, (data, s) => data.Tp = s);
  }

  public ObservableGlobalSaveData(GlobalSaveData globalSaveData) :
    base(globalSaveData)
  {
    UnderlyingData = globalSaveData;
    _currentMap = new ObservableBfNamedId(UnderlyingData.CurrentMap);
    _currentArea = new ObservableBfNamedId(UnderlyingData.CurrentArea);

    CurrentArea
      .WhenValueChanged(x => x.Id)
      .Subscribe(_ => WeakReferenceMessenger.Default.Send(new ValueChangedMessage<ObservableBfNamedId>(CurrentArea)));
  }
}
