using System;
using System.Linq;
using BugFablesLib.SaveData;
using Humanizer;
using static BugFablesLib.SaveData.StatBonusSaveData;

namespace BugFablesSaveEditor.ObservableModels;

public class ObservableStatsBonusSaveData : ObservableModel
{
  public sealed override StatBonusSaveData UnderlyingData { get; }

  public string[] StatBonusTypeNames => Enum.GetNames(typeof(StatBonusType))
    .Select(x => x.Humanize(LetterCasing.Title))
    .ToArray();

  public string TypeName => StatBonusTypeNames[(int)Type];

  public int Target
  {
    get => UnderlyingData.Target;
    set => SetProperty(UnderlyingData.Target, value, UnderlyingData, (data, i) => data.Target = i);
  }

  public StatBonusType Type
  {
    get => UnderlyingData.Type;
    set
    {
      SetProperty(UnderlyingData.Type, value, UnderlyingData, (data, i) => data.Type = i);
      OnPropertyChanged(nameof(TypeName));
    }
  }

  public int Amount
  {
    get => UnderlyingData.Amount;
    set => SetProperty(UnderlyingData.Amount, value, UnderlyingData, (data, i) => data.Amount = i);
  }

  public ObservableStatsBonusSaveData(StatBonusSaveData statBonusSaveData) :
    base(statBonusSaveData)
  {
    UnderlyingData = statBonusSaveData;
  }
}
