using BugFablesLib;
using BugFablesLib.Data;
using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ObservableModels;

public class ObservableBfSaveData : ObservableObject
{
  public readonly BfSaveData _saveData;

  public ViewModelCollection<FlagSaveData, ObservableFlagSaveData> CrystalBerries { get; }

  public ViewModelCollection<EnemyEncounterSaveData, ObservableEnemyEncounterSaveData>
    EnemyEncounters { get; }

  public ViewModelCollection<FlagSaveData, ObservableFlagSaveData> Flags { get; }

  public ViewModelCollection<FlagstringSaveData, ObservableFlagstringSaveData> Flagstrings
  {
    get;
  }

  public ViewModelCollection<FlagvarSaveData, ObservableFlagvarSaveData> Flagvars { get; }
  public ViewModelCollection<BfAnimId, ObservableBfNamedId> Followers { get; }
  public ObservableGlobalSaveData Global { get; }
  public ObservableHeaderSaveData Header { get; }
  public ObservableItemsSaveData Items { get; }
  public ObservableLibrarySaveData Library { get; }

  public ViewModelCollection<BfMedalOnHandSaveData, ObservableMedalOnHandSaveData> Medals
  {
    get;
  }

  public ObservableMedalShopsStockSaveData MedalShopsAvailables { get; }
  public ObservableMedalShopsStockSaveData MedalShopsPools { get; }

  public ViewModelCollection<PartyMemberSaveData, ObservablePartyMemberSaveData> PartyMembers
  {
    get;
  }

  public ObservableBoardQuestsSaveData Quests { get; }
  public ViewModelCollection<FlagSaveData, ObservableFlagSaveData> RegionalFlags { get; }
  public ViewModelCollection<BfMusicSaveData, ObservableMusicSaveData> SamiraSongs { get; }

  public ViewModelCollection<StatBonusSaveData, ObservableStatsBonusSaveData> StatBonuses
  {
    get;
  }

  public ObservableBfSaveData(BfSaveData saveData)
  {
    _saveData = saveData;
    CrystalBerries = new(_saveData.CrystalBerries);
    EnemyEncounters = new(_saveData.EnemyEncounters);
    Flags = new(_saveData.Flags);
    Flagvars = new(_saveData.Flagvars);
    Flagstrings = new(_saveData.Flagstrings);
    Followers = new(_saveData.Followers);
    Medals = new(_saveData.Medals);
    PartyMembers = new(_saveData.PartyMembers);
    RegionalFlags = new(_saveData.RegionalFlags);
    SamiraSongs = new(_saveData.SamiraSongs);
    StatBonuses = new(_saveData.StatBonuses);
    Global = new(_saveData.Global);
    Header = new(_saveData.Header);
    Items = new(_saveData.Items);
    Library = new(_saveData.Library);
    MedalShopsPools = new(_saveData.MedalShopsPools);
    MedalShopsAvailables = new(_saveData.MedalShopsAvailables);
    Quests = new(_saveData.Quests);
  }
}
