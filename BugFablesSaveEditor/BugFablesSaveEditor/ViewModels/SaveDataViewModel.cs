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

  [ObservableProperty]
  private LibraryViewModel _libraryViewModel;

  public SaveDataViewModel(BfSaveData saveData)
  {
    SaveData = saveData;
    ViewModelCollection<FlagSaveData, ObservableFlagSaveData> crystalBerries = new(SaveData.CrystalBerries);
    ViewModelCollection<EnemyEncounterSaveData, ObservableEnemyEncounterSaveData> enemyEncounters =
      new(SaveData.EnemyEncounters);
    ViewModelCollection<BfAnimId, ObservableBfNamedId> followers = new(SaveData.Followers);
    ViewModelCollection<PartyMemberSaveData, ObservablePartyMemberSaveData> partyMembers = new(SaveData.PartyMembers);
    ViewModelCollection<FlagSaveData, ObservableFlagSaveData> flags = new(SaveData.Flags);
    ViewModelCollection<FlagvarSaveData, ObservableFlagvarSaveData> flagvars = new(SaveData.Flagvars);
    ViewModelCollection<FlagstringSaveData, ObservableFlagstringSaveData> flagstrings = new(SaveData.Flagstrings);
    ViewModelCollection<BfMedalOnHandSaveData, ObservableMedalOnHandSaveData> medals = new(SaveData.Medals);
    ViewModelCollection<FlagSaveData, ObservableFlagSaveData> regionalFlags = new(SaveData.RegionalFlags);
    ViewModelCollection<BfMusicSaveData, ObservableMusicSaveData> samiraSongs = new(SaveData.SamiraSongs);
    ViewModelCollection<StatBonusSaveData, ObservableStatsBonusSaveData> statBonuses = new(SaveData.StatBonuses);
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
    _libraryViewModel = new(library);
  }
}
