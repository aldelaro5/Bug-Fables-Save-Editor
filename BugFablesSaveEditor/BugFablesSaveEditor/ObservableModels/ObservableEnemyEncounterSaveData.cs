using BugFablesLib.SaveData;
using Reactive.Bindings;

namespace BugFablesSaveEditor.ObservableModels;

public class ObservableEnemyEncounterSaveData : ObservableModel
{
  public sealed override EnemyEncounterSaveData UnderlyingData { get; }

  public ReactiveProperty<int> NbrSeen { get; }
  public ReactiveProperty<int> NbrDefeated { get; }

  public ObservableEnemyEncounterSaveData(EnemyEncounterSaveData enemyEncounterSaveData) :
    base(enemyEncounterSaveData)
  {
    UnderlyingData = enemyEncounterSaveData;
    NbrSeen = ReactiveProperty.FromObject(UnderlyingData, data => data.NbrSeen);
    NbrDefeated = ReactiveProperty.FromObject(UnderlyingData, data => data.NbrDefeated);
  }
}
