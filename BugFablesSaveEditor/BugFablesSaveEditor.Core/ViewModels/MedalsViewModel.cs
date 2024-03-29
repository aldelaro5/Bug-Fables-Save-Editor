using System;
using System.Collections.ObjectModel;
using BugFablesLib.Data;
using BugFablesLib.SaveData;
using BugFablesSaveEditor.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BugFablesSaveEditor.Core.ViewModels;

public partial class MedalsViewModel : ObservableObject, IDisposable
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

  public void Dispose()
  {
    MedalOnHandSaveData.Dispose();
    MerabShopPool.Dispose();
    ShadesShopPool.Dispose();
    MerabShopAvailables.Dispose();
    ShadesShopAvailables.Dispose();
  }
}
