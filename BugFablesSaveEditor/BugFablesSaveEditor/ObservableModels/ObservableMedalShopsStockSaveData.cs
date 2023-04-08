using BugFablesLib.Data;
using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ObservableModels;

public partial class ObservableMedalShopsStockSaveData : ObservableObject
{
  public MedalShopsStockSaveData Model { get; }

  [ObservableProperty]
  private ViewModelCollection<BfMedal, ObservableBfNamedId> _merab;

  [ObservableProperty]
  private ViewModelCollection<BfMedal, ObservableBfNamedId> _shades;

  public ObservableMedalShopsStockSaveData(MedalShopsStockSaveData itemsSaveData)
  {
    Model = itemsSaveData;
    _merab = new(Model.Merab);
    _shades = new(Model.Shades);
  }
}
