using System.Collections.ObjectModel;
using BugFablesLib.Data;
using BugFablesLib.SaveData;
using BugFablesSaveEditor.ObservableModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BugFablesSaveEditor.ViewModels;

public partial class MedalsViewModel : ObservableObject
{
  [ObservableProperty]
  private ViewModelCollection<BfMedalOnHandSaveData, ObservableMedalOnHandSaveData> _medalOnHandSaveData;

  [ObservableProperty]
  private ViewModelCollection<BfMedal, ObservableBfNamedId> _merabShopPool;

  [ObservableProperty]
  private ViewModelCollection<BfMedal, ObservableBfNamedId> _shadesShopPool;

  [ObservableProperty]
  private ViewModelCollection<BfMedal, ObservableBfNamedId> _merabShopAvailables;

  [ObservableProperty]
  private ViewModelCollection<BfMedal, ObservableBfNamedId> _shadesShopAvailables;

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
    foreach (ObservableMedalOnHandSaveData medal in MedalOnHandSaveData.CollectionView)
      medal.MedalEquipTarget = 0;
  }
}
