using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

public class ObservableEnemyEncounterSaveData : ObservableObject
{
  private readonly EnemyEncounterSaveData _enemyEncounterSaveData;

  public int NbrSeen
  {
    get => _enemyEncounterSaveData.NbrSeen;
    set => SetProperty(_enemyEncounterSaveData.NbrSeen, value, _enemyEncounterSaveData,
      (enemyEncounterSaveData, n) => enemyEncounterSaveData.NbrSeen = n);
  }

  public int NbrDefeated
  {
    get => _enemyEncounterSaveData.NbrDefeated;
    set => SetProperty(_enemyEncounterSaveData.NbrDefeated, value, _enemyEncounterSaveData,
      (enemyEncounterSaveData, n) => enemyEncounterSaveData.NbrDefeated = n);
  }

  public ObservableEnemyEncounterSaveData(EnemyEncounterSaveData enemyEncounterSaveData)
  {
    _enemyEncounterSaveData = enemyEncounterSaveData;
  }
}
