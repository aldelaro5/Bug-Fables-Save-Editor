using System.Collections.Generic;
using BugFablesLib.Data;
using BugFablesLib.SaveData;
using BugFablesSaveEditor.ObservableModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BugFablesSaveEditor.ViewModels;

public partial class MedalsViewModel : ObservableObject
{
  [ObservableProperty]
  private ObservableBfCollection<BfMedalOnHandSaveData, ObservableMedalOnHandSaveData>
    _medalOnHandSaveData;

  [ObservableProperty]
  private ObservableMedalShopsStockSaveData _medalShopsStockPoolSaveData;

  [ObservableProperty]
  private ObservableMedalShopsStockSaveData _medalShopsAvailablePoolSaveData;

  [ObservableProperty]
  private ObservableMedalOnHandSaveData _newMedalOnHand = new(new BfMedalOnHandSaveData());

  [ObservableProperty]
  private ObservableBfNamedId _newPoolMerabMedal = new(new BfMedal());

  [ObservableProperty]
  private ObservableBfNamedId _newPoolShadesMedal = new(new BfMedal());

  [ObservableProperty]
  private ObservableBfNamedId _newAvailableMerabMedal = new(new BfMedal());

  [ObservableProperty]
  private ObservableBfNamedId _newAvailableShadesMedal = new(new BfMedal());

  public MedalsViewModel()
  {
    _medalOnHandSaveData = new(new(), _ => new List<ObservableMedalOnHandSaveData>());
    _medalShopsStockPoolSaveData = new(new());
    _medalShopsAvailablePoolSaveData = new(new());
  }

  public MedalsViewModel(
    ObservableBfCollection<BfMedalOnHandSaveData, ObservableMedalOnHandSaveData>
      medalOnHandSaveData, ObservableMedalShopsStockSaveData medalShopsStockPoolSaveData,
    ObservableMedalShopsStockSaveData medalShopsAvailablePoolSaveData)
  {
    _medalOnHandSaveData = medalOnHandSaveData;
    _medalShopsStockPoolSaveData = medalShopsStockPoolSaveData;
    _medalShopsAvailablePoolSaveData = medalShopsAvailablePoolSaveData;
  }

  [RelayCommand]
  private void UnequipAllMedals()
  {
    foreach (ObservableMedalOnHandSaveData medal in MedalOnHandSaveData)
      medal.MedalEquipTarget.Value = 0;
  }

  [RelayCommand]
  private void AddMedalOnHand(ObservableMedalOnHandSaveData medal) =>
    MedalOnHandSaveData.Add(new(medal.UnderlyingData));

  [RelayCommand]
  private void DeleteMedalOnHand(ObservableMedalOnHandSaveData medal) =>
    MedalOnHandSaveData.Remove(medal);

  [RelayCommand]
  private void AddMedalShopPoolMerab(ObservableBfNamedId medal) =>
    MedalShopsStockPoolSaveData.Merab.Add(new(medal.ToMedal()));

  [RelayCommand]
  private void DeleteMedalShopPoolMerab(ObservableBfNamedId medal) =>
    MedalShopsStockPoolSaveData.Merab.Remove(medal);

  [RelayCommand]
  private void AddMedalShopPoolShades(ObservableBfNamedId medal) =>
    MedalShopsStockPoolSaveData.Shades.Add(new(medal.ToMedal()));

  [RelayCommand]
  private void DeleteMedalShopPoolShades(ObservableBfNamedId medal) =>
    MedalShopsStockPoolSaveData.Shades.Remove(medal);

  [RelayCommand]
  private void AddMedalShopAvailableMerab(ObservableBfNamedId medal) =>
    MedalShopsAvailablePoolSaveData.Merab.Add(new(medal.ToMedal()));

  [RelayCommand]
  private void DeleteMedalShopAvailableMerab(ObservableBfNamedId medal) =>
    MedalShopsAvailablePoolSaveData.Merab.Remove(medal);

  [RelayCommand]
  private void AddMedalShopAvailableShades(ObservableBfNamedId medal) =>
    MedalShopsAvailablePoolSaveData.Shades.Add(new(medal.ToMedal()));

  [RelayCommand]
  private void DeleteMedalShopAvailableShades(ObservableBfNamedId medal) =>
    MedalShopsAvailablePoolSaveData.Shades.Remove(medal);
}
