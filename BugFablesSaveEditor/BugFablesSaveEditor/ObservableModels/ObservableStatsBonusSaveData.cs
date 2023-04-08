using System;
using System.Linq;
using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;
using Humanizer;
using static BugFablesLib.SaveData.StatBonusSaveData;

namespace BugFablesSaveEditor.ObservableModels;

public class ObservableStatsBonusSaveData : ObservableObject, IModelWrapper<StatBonusSaveData>
{
  public StatBonusSaveData Model { get; }
  public static IModelWrapper<StatBonusSaveData> Factory(StatBonusSaveData model) =>
    new ObservableStatsBonusSaveData(model);

  public string[] StatBonusTypeNames => Enum.GetNames(typeof(StatBonusType))
    .Select(x => x.Humanize(LetterCasing.Title))
    .ToArray();

  public string TypeName => StatBonusTypeNames[(int)Type];

  public int Target
  {
    get => Model.Target;
    set => SetProperty(Model.Target, value, Model, (data, i) => data.Target = i);
  }

  public StatBonusType Type
  {
    get => Model.Type;
    set
    {
      SetProperty(Model.Type, value, Model, (data, i) => data.Type = i);
      OnPropertyChanged(nameof(TypeName));
    }
  }

  public int Amount
  {
    get => Model.Amount;
    set => SetProperty(Model.Amount, value, Model, (data, i) => data.Amount = i);
  }

  public ObservableStatsBonusSaveData(StatBonusSaveData statBonusSaveData) => Model = statBonusSaveData;
}
