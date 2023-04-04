using System;
using System.Linq;
using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using DynamicData.Binding;
using Humanizer;
using Reactive.Bindings;
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

  public ReactiveProperty<int> BerryCount { get; }
  public ReactiveProperty<int> Exp { get; }
  public ReactiveProperty<int> MaxMp { get; }
  public ReactiveProperty<int> MaxTp { get; }
  public ReactiveProperty<int> Mp { get; }
  public ReactiveProperty<int> NbrMaxItemsInventory { get; }
  public ReactiveProperty<int> NbrMaxItemsStorage { get; }
  public ReactiveProperty<int> NeededExp { get; }
  public ReactiveProperty<int> PlayTimeHours { get; }
  public ReactiveProperty<int> PlayTimeMinutes { get; }
  public ReactiveProperty<int> PlayTimeSeconds { get; }
  public ReactiveProperty<int> Rank { get; }
  public ReactiveProperty<SaveProgressIcon> LastProgressIcon { get; }
  public ReactiveProperty<int> Tp { get; }

  public ObservableGlobalSaveData(GlobalSaveData globalSaveDatal) :
    base(globalSaveDatal)
  {
    UnderlyingData = globalSaveDatal;
    _currentMap = new ObservableBfNamedId(UnderlyingData.CurrentMap);
    _currentArea = new ObservableBfNamedId(UnderlyingData.CurrentArea);
    Rank = ReactiveProperty.FromObject(UnderlyingData, data => data.Rank);
    Exp = ReactiveProperty.FromObject(UnderlyingData, data => data.Exp);
    NeededExp = ReactiveProperty.FromObject(UnderlyingData, data => data.NeededExp);
    MaxTp = ReactiveProperty.FromObject(UnderlyingData, data => data.MaxTp);
    Tp = ReactiveProperty.FromObject(UnderlyingData, data => data.Tp);
    BerryCount = ReactiveProperty.FromObject(UnderlyingData, data => data.BerryCount);
    Mp = ReactiveProperty.FromObject(UnderlyingData, data => data.Mp);
    MaxMp = ReactiveProperty.FromObject(UnderlyingData, data => data.MaxMp);
    NbrMaxItemsInventory =
      ReactiveProperty.FromObject(UnderlyingData, data => data.NbrMaxItemsInventory);
    NbrMaxItemsStorage =
      ReactiveProperty.FromObject(UnderlyingData, data => data.NbrMaxItemsStorage);
    PlayTimeHours = ReactiveProperty.FromObject(UnderlyingData, data => data.PlayTimeHours);
    PlayTimeMinutes = ReactiveProperty.FromObject(UnderlyingData, data => data.PlayTimeMinutes);
    PlayTimeSeconds = ReactiveProperty.FromObject(UnderlyingData, data => data.PlayTimeSeconds);
    LastProgressIcon =
      ReactiveProperty.FromObject(UnderlyingData, data => data.LastProgressIcon);

    CurrentArea.Id
      .WhenValueChanged(x => x.Value)
      .Subscribe(_ =>
        WeakReferenceMessenger.Default.Send(
          new ValueChangedMessage<ObservableBfNamedId>(CurrentArea)));
  }
}
