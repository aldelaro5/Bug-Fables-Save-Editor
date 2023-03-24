using System.Linq;
using BugFablesLib.Data;
using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ObservableModels;

[ObservableObject]
public partial class ObservableMedalShopsStockSaveData : ObservableModel
{
  public sealed override MedalShopsStockSaveData UnderlyingData { get; }

  [ObservableProperty]
  private ObservableBfCollection<BfMedal, ObservableBfResource> _merab;

  [ObservableProperty]
  private ObservableBfCollection<BfMedal, ObservableBfResource> _shades;

  public ObservableMedalShopsStockSaveData(MedalShopsStockSaveData itemsSaveData) :
    base(itemsSaveData)
  {
    UnderlyingData = itemsSaveData;
    _merab = new(UnderlyingData.Merab,
      medals => medals.Select(x => new ObservableBfResource(x)).ToList());
    _shades = new(UnderlyingData.Shades,
      medals => medals.Select(x => new ObservableBfResource(x)).ToList());
  }
}
