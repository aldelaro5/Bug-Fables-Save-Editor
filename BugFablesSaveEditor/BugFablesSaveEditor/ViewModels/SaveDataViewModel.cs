using System;
using BugFablesLib;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

public partial class SaveDataViewModel : ObservableObject, IDisposable
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
    _partyViewModel = new(saveData.PartyMembers, saveData.Followers);
    _statsViewModel = new(saveData.StatBonuses, saveData.PartyMembers, saveData.Global);
    _questsViewModel = new(saveData.Quests);
    _itemsViewModel = new(saveData.Items);
    _medalsViewModel = new(saveData.Medals, saveData.MedalShopsPools, saveData.MedalShopsAvailables);
    _songsViewModel = new(saveData.SamiraSongs);
    _crystalBerriesViewModel = new(saveData.CrystalBerries);
    _flagsViewModel = new(saveData.Flags, saveData.Flagvars, saveData.Flagstrings, saveData.RegionalFlags);
    _globalViewModel = new(saveData.Global, saveData.Header);
    _libraryViewModel = new(saveData.Library);
  }

  public void Dispose()
  {
    GlobalViewModel.Dispose();
    FlagsViewModel.Dispose();
    LibraryViewModel.Dispose();
    CrystalBerriesViewModel.Dispose();
  }
}
