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
    CrystalBerries = new(_saveData.CrystalBerries, x => new ObservableFlagSaveData(x));
    EnemyEncounters = new(_saveData.EnemyEncounters, x => new ObservableEnemyEncounterSaveData(x));
    Flags = new(_saveData.Flags, x => new ObservableFlagSaveData(x));
    Flagvars = new(_saveData.Flagvars, x => new ObservableFlagvarSaveData(x));
    Flagstrings = new(_saveData.Flagstrings, x => new ObservableFlagstringSaveData(x));
    Followers = new(_saveData.Followers, x => new ObservableBfNamedId(x));
    Medals = new(_saveData.Medals, x => new ObservableMedalOnHandSaveData(x));
    PartyMembers = new(_saveData.PartyMembers, x => new ObservablePartyMemberSaveData(x));
    RegionalFlags = new(_saveData.RegionalFlags, x => new ObservableFlagSaveData(x));
    SamiraSongs = new(_saveData.SamiraSongs, x => new ObservableMusicSaveData(x));
    StatBonuses = new(_saveData.StatBonuses, x => new ObservableStatsBonusSaveData(x));
    Global = new(_saveData.Global);
    Header = new(_saveData.Header);
    Items = new(_saveData.Items);
    Library = new(_saveData.Library);
    MedalShopsPools = new(_saveData.MedalShopsPools);
    MedalShopsAvailables = new(_saveData.MedalShopsAvailables);
    Quests = new(_saveData.Quests);
  }
}
