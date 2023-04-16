using System;
using System.Linq;
using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;
using Humanizer;
using static BugFablesLib.SaveData.StatBonusSaveData;

namespace BugFablesSaveEditor.Models;

public class StatsBonusSaveDataModel : ObservableObject, IModelWrapper<StatBonusSaveData>
{
  public StatBonusSaveData Model { get; }

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

  public string TypeName => StatBonusTypeNames[(int)Type];

  public static IModelWrapper<StatBonusSaveData> WrapModel(StatBonusSaveData model) =>
    new StatsBonusSaveDataModel(model);

  public static IModelWrapper<StatBonusSaveData> WrapNewModel(StatBonusSaveData model) =>
    new StatsBonusSaveDataModel(new StatBonusSaveData
    {
      Target = model.Target,
      Type = model.Type,
      Amount = model.Amount
    });

  public StatsBonusSaveDataModel(StatBonusSaveData statBonusSaveData) => Model = statBonusSaveData;

  private string[] StatBonusTypeNames => Enum.GetNames(typeof(StatBonusType))
    .Select(x => x.Humanize(LetterCasing.Title))
    .ToArray();
}
