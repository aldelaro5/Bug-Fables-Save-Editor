using System;
using System.Linq;
using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;
using Humanizer;
using Reactive.Bindings;
using static BugFablesLib.SaveData.StatBonusSaveData;

namespace BugFablesSaveEditor.ViewModels;

[ObservableObject]
public partial class ObservableStatsBonusSaveData : BfObservable
{
  public sealed override StatBonusSaveData UnderlyingData { get; }

  public string[] StatBonusTypeNames => Enum.GetNames(typeof(StatBonusType))
    .Select(x => x.Humanize(LetterCasing.Title))
    .ToArray();

  [ObservableProperty]
  private ObservableBfResource _target;

  public ReactiveProperty<int> Type { get; }
  public ReactiveProperty<int> Amount { get; }

  public ObservableStatsBonusSaveData(StatBonusSaveData statBonusSaveData) :
    base(statBonusSaveData)
  {
    UnderlyingData = statBonusSaveData;
    _target = new ObservableBfResource(statBonusSaveData.Target);
    Type = ReactiveProperty.FromObject(UnderlyingData, data => (int)data.Type);
    Amount = ReactiveProperty.FromObject(UnderlyingData, data => data.Amount);
  }
}
