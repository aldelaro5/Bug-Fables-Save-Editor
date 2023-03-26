using System.Linq;
using BugFablesLib;
using BugFablesLib.Data;
using BugFablesLib.SaveData;
using BugFablesSaveEditor.ObservableModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

public partial class SaveDataViewModel : ObservableObject
{
  public readonly BfSaveData _saveData;

  [ObservableProperty]
  private GlobalViewModel _globalViewModel;

  [ObservableProperty]
  private PartyViewModel _partyViewModel;

  public SaveDataViewModel(BfSaveData saveData)
  {
    _saveData = saveData;
    ObservableBfCollection<FlagSaveData, ObservableFlagSaveData> CrystalBerries = new(
      _saveData.CrystalBerries,
      cbs => cbs.Select(x => new ObservableFlagSaveData(x)).ToList());
    ObservableBfCollection<EnemyEncounterSaveData, ObservableEnemyEncounterSaveData>
      EnemyEncounters = new(_saveData.EnemyEncounters,
        cbs => cbs.Select(x => new ObservableEnemyEncounterSaveData(x)).ToList());
    ObservableBfCollection<BfAnimId, ObservableBfNamedId> followers = new(_saveData.Followers,
      cbs => cbs.Select(x => new ObservableBfNamedId(x)).ToList());
    ObservableBfCollection<PartyMemberSaveData, ObservablePartyMemberSaveData> partyMembers = new(
      _saveData.PartyMembers,
      cbs => cbs.Select(x => new ObservablePartyMemberSaveData(x)).ToList());
    // var Flags = new(_saveData.Flags, cbs => cbs.Select(x => new ObservableFlagSaveData(x)).ToList());
    // var Flagvars = new(_saveData.Flagvars,
    //   cbs => cbs.Select(x => new ObservableFlagvarSaveData(x)).ToList());
    // var Flagstrings = new(_saveData.Flagstrings,
    //   cbs => cbs.Select(x => new ObservableFlagstringSaveData(x)).ToList());
    // var Medals = new(_saveData.Medals,
    //   cbs => cbs.Select(x => new ObservableMedalOnHandSaveData(x)).ToList());
    // var RegionalFlags = new(_saveData.RegionalFlags,
    //   cbs => cbs.Select(x => new ObservableFlagSaveData(x)).ToList());
    // var SamiraSongs = new(_saveData.SamiraSongs,
    //   cbs => cbs.Select(x => new ObservableMusicSaveData(x)).ToList());
    // var StatBonuses = new(_saveData.StatBonuses,
    //   cbs => cbs.Select(x => new ObservableStatsBonusSaveData(x)).ToList());
    // var Items = new(_saveData.Items);
    // var Library = new(_saveData.Library);
    // var MedalShopsPools = new(_saveData.MedalShopsPools);
    // var MedalShopsAvailables = new(_saveData.MedalShopsAvailables);
    // var Quests = new(_saveData.Quests);
    _globalViewModel = new(new(_saveData.Global), new(_saveData.Header));
    _partyViewModel = new(partyMembers, followers);
  }
}
