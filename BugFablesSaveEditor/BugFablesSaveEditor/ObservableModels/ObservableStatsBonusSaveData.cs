using System;
using System.Linq;
using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;
using Humanizer;
using Reactive.Bindings;
using static BugFablesLib.SaveData.StatBonusSaveData;

namespace BugFablesSaveEditor.ObservableModels;

[ObservableObject]
public partial class ObservableStatsBonusSaveData : ObservableModel
{
  public sealed override StatBonusSaveData UnderlyingData { get; }

  public string[] StatBonusTypeNames => Enum.GetNames(typeof(StatBonusType))
    .Select(x => x.Humanize(LetterCasing.Title))
    .ToArray();

  [ObservableProperty]
  private ObservableBfNamedId _target;

  public ReactiveProperty<StatBonusType> Type { get; }
  public ReactiveProperty<int> Amount { get; }

  public ObservableStatsBonusSaveData(StatBonusSaveData statBonusSaveData) :
    base(statBonusSaveData)
  {
    UnderlyingData = statBonusSaveData;
    _target = new ObservableBfNamedId(statBonusSaveData.Target);
    Type = ReactiveProperty.FromObject(UnderlyingData, data => data.Type);
    Amount = ReactiveProperty.FromObject(UnderlyingData, data => data.Amount);
  }
}
