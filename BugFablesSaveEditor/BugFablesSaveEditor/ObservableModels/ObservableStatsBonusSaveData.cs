using System;
using System.Linq;
using BugFablesLib.SaveData;
using Humanizer;
using Reactive.Bindings;
using static BugFablesLib.SaveData.StatBonusSaveData;

namespace BugFablesSaveEditor.ObservableModels;

public partial class ObservableStatsBonusSaveData : ObservableModel
{
  public sealed override StatBonusSaveData UnderlyingData { get; }

  public string[] StatBonusTypeNames => Enum.GetNames(typeof(StatBonusType))
    .Select(x => x.Humanize(LetterCasing.Title))
    .ToArray();

  public ReactiveProperty<int> Target { get; }
  public string TypeName => StatBonusTypeNames[(int)Type.Value];
  public ReactiveProperty<StatBonusType> Type { get; }
  public ReactiveProperty<int> Amount { get; }

  public ObservableStatsBonusSaveData(StatBonusSaveData statBonusSaveData) :
    base(statBonusSaveData)
  {
    UnderlyingData = statBonusSaveData;
    Type = ReactiveProperty.FromObject(UnderlyingData, data => data.Type);
    Type.Subscribe(_ => OnPropertyChanged(nameof(TypeName)));
    Target = ReactiveProperty.FromObject(UnderlyingData, data => data.Target);
    Amount = ReactiveProperty.FromObject(UnderlyingData, data => data.Amount);
  }
}
