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
  private ObservableMedalShopsStockSaveData _medalShopsStockPoolSaveData;

  [ObservableProperty]
  private ObservableMedalShopsStockSaveData _medalShopsAvailablePoolSaveData;

  public MedalsViewModel()
  {
    _medalOnHandSaveData = new(new(), x => new ObservableMedalOnHandSaveData(x));
    _medalShopsStockPoolSaveData = new(new());
    _medalShopsAvailablePoolSaveData = new(new());
  }

  public MedalsViewModel(
    ViewModelCollection<BfMedalOnHandSaveData, ObservableMedalOnHandSaveData>
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
    foreach (ObservableMedalOnHandSaveData medal in MedalOnHandSaveData.CollectionView)
      medal.MedalEquipTarget = 0;
  }
}
