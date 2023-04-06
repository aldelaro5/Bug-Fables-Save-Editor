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

public partial class ObservableGlobalSaveData : ObservableObject
{
  public GlobalSaveData Model { get; }

  [ObservableProperty]
  private ObservableBfNamedId _currentArea;

  [ObservableProperty]
  private ObservableBfNamedId _currentMap;

  public string[] SaveProgressIconNames =>
    Enum.GetNames(typeof(SaveProgressIcon)).Select(x => x.Humanize(LetterCasing.Title)).ToArray();

  public int BerryCount
  {
    get => Model.BerryCount;
    set => SetProperty(Model.BerryCount, value, Model, (data, s) => data.BerryCount = s);
  }

  public int Exp
  {
    get => Model.Exp;
    set => SetProperty(Model.Exp, value, Model, (data, s) => data.Exp = s);
  }

  public int MaxMp
  {
    get => Model.MaxMp;
    set => SetProperty(Model.MaxMp, value, Model, (data, s) => data.MaxMp = s);
  }

  public int MaxTp
  {
    get => Model.MaxTp;
    set => SetProperty(Model.MaxTp, value, Model, (data, s) => data.MaxTp = s);
  }

  public int Mp
  {
    get => Model.Mp;
    set => SetProperty(Model.Mp, value, Model, (data, s) => data.Mp = s);
  }

  public int NbrMaxItemsInventory
  {
    get => Model.NbrMaxItemsInventory;
    set => SetProperty(Model.NbrMaxItemsInventory, value, Model,
      (data, s) => data.NbrMaxItemsInventory = s);
  }

  public int NbrMaxItemsStorage
  {
    get => Model.NbrMaxItemsStorage;
    set => SetProperty(Model.NbrMaxItemsStorage, value, Model,
      (data, s) => data.NbrMaxItemsStorage = s);
  }

  public int NeededExp
  {
    get => Model.NeededExp;
    set => SetProperty(Model.NeededExp, value, Model, (data, s) => data.NeededExp = s);
  }

  public int PlayTimeHours
  {
    get => Model.PlayTimeHours;
    set => SetProperty(Model.PlayTimeHours, value, Model, (data, s) => data.PlayTimeHours = s);
  }

  public int PlayTimeMinutes
  {
    get => Model.PlayTimeMinutes;
    set => SetProperty(Model.PlayTimeMinutes, value, Model, (data, s) => data.PlayTimeMinutes = s);
  }

  public int PlayTimeSeconds
  {
    get => Model.PlayTimeSeconds;
    set => SetProperty(Model.PlayTimeSeconds, value, Model, (data, s) => data.PlayTimeSeconds = s);
  }

  public int Rank
  {
    get => Model.Rank;
    set => SetProperty(Model.Rank, value, Model, (data, s) => data.Rank = s);
  }

  public SaveProgressIcon LastProgressIcon
  {
    get => Model.LastProgressIcon;
    set => SetProperty(Model.LastProgressIcon, value, Model, (data, s) => data.LastProgressIcon = s);
  }

  public int Tp
  {
    get => Model.Tp;
    set => SetProperty(Model.Tp, value, Model, (data, s) => data.Tp = s);
  }

  public ObservableGlobalSaveData(GlobalSaveData globalSaveData)
  {
    Model = globalSaveData;
    _currentMap = new ObservableBfNamedId(Model.CurrentMap);
    _currentArea = new ObservableBfNamedId(Model.CurrentArea);

    CurrentArea
      .WhenValueChanged(x => x.Id)
      .Subscribe(_ => WeakReferenceMessenger.Default.Send(new ValueChangedMessage<ObservableBfNamedId>(CurrentArea)));
  }
}
