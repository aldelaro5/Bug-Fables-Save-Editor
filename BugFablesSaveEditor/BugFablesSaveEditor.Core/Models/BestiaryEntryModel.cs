using BugFablesLib.SaveData;

namespace BugFablesSaveEditor.Core.Models;

public class BestiaryEntryModel : FlagSaveDataModel
{
  private EnemyEncounterSaveData EncounterSaveData { get; }

  public int NbrSeen
  {
    get => EncounterSaveData.NbrSeen;
    set => SetProperty(EncounterSaveData.NbrSeen, value, EncounterSaveData, (data, i) => data.NbrSeen = i);
  }

  public int NbrDefeated
  {
    get => EncounterSaveData.NbrDefeated;
    set => SetProperty(EncounterSaveData.NbrDefeated, value, EncounterSaveData, (data, i) => data.NbrDefeated = i);
  }

  public BestiaryEntryModel(FlagSaveData flagSaveData, EnemyEncounterSaveData enemyEncounterSaveData) :
    base(flagSaveData) => EncounterSaveData = enemyEncounterSaveData;
}
