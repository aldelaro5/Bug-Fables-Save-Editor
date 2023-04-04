using System.Linq;
using BugFablesLib;
using BugFablesLib.Data;
using BugFablesLib.SaveData;
using BugFablesSaveEditor.ObservableModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

public partial class SaveDataViewModel : ObservableObject
{
  public readonly BfSaveData SaveData;

  [ObservableProperty]
  private GlobalViewModel _globalViewModel;

  [ObservableProperty]
  private PartyViewModel _partyViewModel;

  [ObservableProperty]
  private StatsViewModel _statsViewModel;

  [ObservableProperty]
  private QuestsViewModel _questsViewModel;

  [ObservableProperty]
  private ItemsViewModel _itemsViewModel;

  [ObservableProperty]
  private MedalsViewModel _medalsViewModel;

  [ObservableProperty]
  private SongsViewModel _songsViewModel;

  [ObservableProperty]
  private CrystalBerriesViewModel _crystalBerriesViewModel;

  [ObservableProperty]
  private FlagsViewModel _flagsViewModel;

  public SaveDataViewModel(BfSaveData saveData)
  {
    SaveData = saveData;
    ObservableBfCollection<FlagSaveData, ObservableFlagSaveData> crystalBerries = new(
      SaveData.CrystalBerries,
      cbs => cbs.Select(x => new ObservableFlagSaveData(x)).ToList());
    ObservableBfCollection<EnemyEncounterSaveData, ObservableEnemyEncounterSaveData>
      enemyEncounters = new(SaveData.EnemyEncounters,
        cbs => cbs.Select(x => new ObservableEnemyEncounterSaveData(x)).ToList());
    ObservableBfCollection<BfAnimId, ObservableBfNamedId> followers = new(SaveData.Followers,
      cbs => cbs.Select(x => new ObservableBfNamedId(x)).ToList());
    ObservableBfCollection<PartyMemberSaveData, ObservablePartyMemberSaveData> partyMembers = new(
      SaveData.PartyMembers,
      cbs => cbs.Select(x => new ObservablePartyMemberSaveData(x)).ToList());
    ObservableBfCollection<FlagSaveData, ObservableFlagSaveData> flags = new(SaveData.Flags,
      cbs => cbs.Select(x => new ObservableFlagSaveData(x)).ToList());
    ObservableBfCollection<FlagvarSaveData, ObservableFlagvarSaveData> flagvars = new(
      SaveData.Flagvars,
      cbs => cbs.Select(x => new ObservableFlagvarSaveData(x)).ToList());
    ObservableBfCollection<FlagstringSaveData, ObservableFlagstringSaveData> flagstrings = new(
      SaveData.Flagstrings,
      cbs => cbs.Select(x => new ObservableFlagstringSaveData(x)).ToList());
    ObservableBfCollection<BfMedalOnHandSaveData, ObservableMedalOnHandSaveData> medals = new(
      SaveData.Medals,
      cbs => cbs.Select(x => new ObservableMedalOnHandSaveData(x)).ToList());
    ObservableBfCollection<FlagSaveData, ObservableFlagSaveData> regionalFlags = new(
      SaveData.RegionalFlags,
      cbs => cbs.Select(x => new ObservableFlagSaveData(x)).ToList());
    ObservableBfCollection<BfMusicSaveData, ObservableMusicSaveData> samiraSongs = new(
      SaveData.SamiraSongs,
      cbs => cbs.Select(x => new ObservableMusicSaveData(x)).ToList());
    ObservableBfCollection<StatBonusSaveData, ObservableStatsBonusSaveData> statBonuses = new(
      SaveData.StatBonuses,
      cbs => cbs.Select(x => new ObservableStatsBonusSaveData(x)).ToList());
    ObservableItemsSaveData items = new(SaveData.Items);
    ObservableLibrarySaveData library = new(SaveData.Library);
    ObservableMedalShopsStockSaveData medalShopsPools = new(SaveData.MedalShopsPools);
    ObservableMedalShopsStockSaveData medalShopsAvailables = new(SaveData.MedalShopsAvailables);
    ObservableBoardQuestsSaveData quests = new(SaveData.Quests);
    _partyViewModel = new(partyMembers, followers);
    _statsViewModel = new(statBonuses, partyMembers, new(SaveData.Global));
    _questsViewModel = new QuestsViewModel(quests);
    _itemsViewModel = new ItemsViewModel(items);
    _medalsViewModel = new MedalsViewModel(medals, medalShopsPools, medalShopsAvailables);
    _songsViewModel = new SongsViewModel(samiraSongs);
    _crystalBerriesViewModel = new CrystalBerriesViewModel(crystalBerries);
    _flagsViewModel = new FlagsViewModel(flags, flagvars, flagstrings, regionalFlags);
    _globalViewModel = new(new(SaveData.Global), new(SaveData.Header));
  }
}
