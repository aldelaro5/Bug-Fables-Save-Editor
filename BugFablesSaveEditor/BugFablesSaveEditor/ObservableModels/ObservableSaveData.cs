using System.Linq;
using BugFablesLib;
using BugFablesLib.Data;
using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ObservableModels;

public class ObservableBfSaveData : ObservableObject
{
  public readonly BfSaveData _saveData;

  public ObservableBfCollection<FlagSaveData, ObservableFlagSaveData> CrystalBerries { get; }

  public ObservableBfCollection<EnemyEncounterSaveData, ObservableEnemyEncounterSaveData>
    EnemyEncounters { get; }

  public ObservableBfCollection<FlagSaveData, ObservableFlagSaveData> Flags { get; }

  public ObservableBfCollection<FlagstringSaveData, ObservableFlagstringSaveData> Flagstrings
  {
    get;
  }

  public ObservableBfCollection<FlagvarSaveData, ObservableFlagvarSaveData> Flagvars { get; }
  public ObservableBfCollection<BfAnimId, ObservableBfNamedId> Followers { get; }
  public ObservableGlobalSaveData Global { get; }
  public ObservableHeaderSaveData Header { get; }
  public ObservableItemsSaveData Items { get; }
  public ObservableLibrarySaveData Library { get; }
  public ObservableBfCollection<BfMedalOnHandSaveData, ObservableMedalOnHandSaveData> Medals { get; }
  public ObservableMedalShopsStockSaveData MedalShopsAvailables { get; }
  public ObservableMedalShopsStockSaveData MedalShopsPools { get; }

  public ObservableBfCollection<PartyMemberSaveData, ObservablePartyMemberSaveData> PartyMembers
  {
    get;
  }

  public ObservableBoardQuestsSaveData Quests { get; }
  public ObservableBfCollection<FlagSaveData, ObservableFlagSaveData> RegionalFlags { get; }
  public ObservableBfCollection<BfMusicSaveData, ObservableMusicSaveData> SamiraSongs { get; }

  public ObservableBfCollection<StatBonusSaveData, ObservableStatsBonusSaveData> StatBonuses
  {
    get;
  }

  public ObservableBfSaveData(BfSaveData saveData)
  {
    _saveData = saveData;
    CrystalBerries = new(_saveData.CrystalBerries,
      cbs => cbs.Select(x => new ObservableFlagSaveData(x)).ToList());
    EnemyEncounters = new(_saveData.EnemyEncounters,
      cbs => cbs.Select(x => new ObservableEnemyEncounterSaveData(x)).ToList());
    Flags = new(_saveData.Flags, cbs => cbs.Select(x => new ObservableFlagSaveData(x)).ToList());
    Flagvars = new(_saveData.Flagvars,
      cbs => cbs.Select(x => new ObservableFlagvarSaveData(x)).ToList());
    Flagstrings = new(_saveData.Flagstrings,
      cbs => cbs.Select(x => new ObservableFlagstringSaveData(x)).ToList());
    Followers = new(_saveData.Followers,
      cbs => cbs.Select(x => new ObservableBfNamedId(x)).ToList());
    Medals = new(_saveData.Medals,
      cbs => cbs.Select(x => new ObservableMedalOnHandSaveData(x)).ToList());
    PartyMembers = new(_saveData.PartyMembers,
      cbs => cbs.Select(x => new ObservablePartyMemberSaveData(x)).ToList());
    RegionalFlags = new(_saveData.RegionalFlags,
      cbs => cbs.Select(x => new ObservableFlagSaveData(x)).ToList());
    SamiraSongs = new(_saveData.SamiraSongs,
      cbs => cbs.Select(x => new ObservableMusicSaveData(x)).ToList());
    StatBonuses = new(_saveData.StatBonuses,
      cbs => cbs.Select(x => new ObservableStatsBonusSaveData(x)).ToList());
    Global = new(_saveData.Global);
    Header = new(_saveData.Header);
    Items = new(_saveData.Items);
    Library = new(_saveData.Library);
    MedalShopsPools = new(_saveData.MedalShopsPools);
    MedalShopsAvailables = new(_saveData.MedalShopsAvailables);
    Quests = new(_saveData.Quests);
  }
}
