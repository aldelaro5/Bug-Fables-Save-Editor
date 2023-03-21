using System.Linq;
using BugFablesLib.Data;
using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

[ObservableObject]
public partial class ObservableMedalShopsStockSaveData : BfObservable
{
  private readonly MedalShopsStockSaveData _itemsQuestsSaveData;

  [ObservableProperty]
  private ObservableBfCollection<BfMedal, ObservableBfResource> _merab;
  [ObservableProperty]
  private ObservableBfCollection<BfMedal, ObservableBfResource> _shades;

  public ObservableMedalShopsStockSaveData(MedalShopsStockSaveData itemsSaveData) :
    base(itemsSaveData)
  {
    _itemsQuestsSaveData = itemsSaveData;
    _merab = new(_itemsQuestsSaveData.Merab,
      medals => medals.Select(x => new ObservableBfResource(x)).ToList());
    _shades = new(_itemsQuestsSaveData.Shades,
      medals => medals.Select(x => new ObservableBfResource(x)).ToList());
  }
}
