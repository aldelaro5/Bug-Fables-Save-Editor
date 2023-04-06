using BugFablesLib.SaveData;

namespace BugFablesSaveEditor.ObservableModels;

public class ObservableEnemyEncounterSaveData : ObservableModel
{
  public sealed override EnemyEncounterSaveData UnderlyingData { get; }

  public int NbrSeen
  {
    get => UnderlyingData.NbrSeen;
    set => SetProperty(UnderlyingData.NbrSeen, value, UnderlyingData, (data, i) => data.NbrSeen = i);
  }

  public int NbrDefeated
  {
    get => UnderlyingData.NbrDefeated;
    set => SetProperty(UnderlyingData.NbrDefeated, value, UnderlyingData, (data, i) => data.NbrDefeated = i);
  }

  public ObservableEnemyEncounterSaveData(EnemyEncounterSaveData enemyEncounterSaveData) :
    base(enemyEncounterSaveData)
  {
    UnderlyingData = enemyEncounterSaveData;
  }
}
