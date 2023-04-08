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

  public MedalsViewModel() : this(new(new()), new(new()), new(new())) { }

  public MedalsViewModel(ViewModelCollection<BfMedalOnHandSaveData, ObservableMedalOnHandSaveData> medalOnHandSaveData,
                         ObservableMedalShopsStockSaveData medalShopsStockPoolSaveData,
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
