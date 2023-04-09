using System;
using BugFablesLib;
using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

public partial class SaveDataViewModel : ObservableObject, IDisposable
{
  public readonly BfSaveData SaveData;
  private const int Nbr11xFlagSlots = 750;
  private const int Nbr11xFlagvarSlots = 70;
  private const int NbrRegionalSlots = 100;
  private const int NbrFlagstringSlots = 15;
  private const int NbrCrystalBerries = 50;

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

  public SaveDataViewModel(BfSaveData saveData, bool newFile)
  {
    SaveData = saveData;

    // Assume that a new file is a 1.1.x save, if we add support for versions, this will be need to be changed
    if (newFile)
    {
      for (int i = 0; i < Nbr11xFlagSlots; i++)
        SaveData.Flags.Add(new());
      for (int i = 0; i < Nbr11xFlagvarSlots; i++)
        SaveData.Flagvars.Add(new());
      for (int i = 0; i < NbrFlagstringSlots; i++)
        SaveData.Flagstrings.Add(new());
      for (int i = 0; i < NbrRegionalSlots; i++)
        SaveData.RegionalFlags.Add(new());
      for (int i = 0; i < NbrCrystalBerries; i++)
        SaveData.CrystalBerries.Add(new());
    }

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
    PartyViewModel.Dispose();
    StatsViewModel.Dispose();
    QuestsViewModel.Dispose();
    ItemsViewModel.Dispose();
    MedalsViewModel.Dispose();
    SongsViewModel.Dispose();
    CrystalBerriesViewModel.Dispose();
    FlagsViewModel.Dispose();
    LibraryViewModel.Dispose();
  }
}
