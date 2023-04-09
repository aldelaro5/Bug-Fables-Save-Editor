using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.Models;

public class EnemyEncounterSaveDataModel : ObservableObject, IModelWrapper<EnemyEncounterSaveData>
{
  public EnemyEncounterSaveData Model { get; }

  public int NbrSeen
  {
    get => Model.NbrSeen;
    set => SetProperty(Model.NbrSeen, value, Model, (data, i) => data.NbrSeen = i);
  }

  public int NbrDefeated
  {
    get => Model.NbrDefeated;
    set => SetProperty(Model.NbrDefeated, value, Model, (data, i) => data.NbrDefeated = i);
  }

  public static IModelWrapper<EnemyEncounterSaveData> WrapModel(EnemyEncounterSaveData model) =>
    new EnemyEncounterSaveDataModel(model);

  private EnemyEncounterSaveDataModel(EnemyEncounterSaveData enemyEncounterSaveData) =>
    Model = enemyEncounterSaveData;
}
