using System.Collections.ObjectModel;
using BugFablesLib.Data;
using BugFablesLib.SaveData;
using BugFablesSaveEditor.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BugFablesSaveEditor.ViewModels;

public partial class MedalsViewModel : ObservableObject
{
  [ObservableProperty]
  private ViewModelCollection<BfMedalOnHandSaveData, MedalOnHandSaveDataModel> _medalOnHandSaveData;

  [ObservableProperty]
  private ViewModelCollection<BfMedal, BfNamedIdModel> _merabShopPool;

  [ObservableProperty]
  private ViewModelCollection<BfMedal, BfNamedIdModel> _shadesShopPool;

  [ObservableProperty]
  private ViewModelCollection<BfMedal, BfNamedIdModel> _merabShopAvailables;

  [ObservableProperty]
  private ViewModelCollection<BfMedal, BfNamedIdModel> _shadesShopAvailables;

  public MedalsViewModel() : this(new(), new(), new()) { }

  public MedalsViewModel(Collection<BfMedalOnHandSaveData> medalOnHandSaveData,
                         MedalShopsStockSaveData shopPoolSaveData,
                         MedalShopsStockSaveData shopAvailableSaveData)
  {
    _medalOnHandSaveData = new(medalOnHandSaveData);
    _merabShopPool = new(shopPoolSaveData.Merab);
    _shadesShopPool = new(shopPoolSaveData.Shades);
    _merabShopAvailables = new(shopAvailableSaveData.Merab);
    _shadesShopAvailables = new(shopAvailableSaveData.Shades);
  }

  [RelayCommand]
  private void UnequipAllMedals()
  {
    foreach (MedalOnHandSaveDataModel medal in MedalOnHandSaveData.Collection)
      medal.MedalEquipTarget = 0;
  }
}
