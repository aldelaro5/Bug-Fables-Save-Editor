using System.Linq;
using BugFablesLib.Data;
using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

[ObservableObject]
public partial class ObservableItemsSaveData : BfObservable
{
  private readonly ItemsSaveData _itemsQuestsSaveData;

  [ObservableProperty]
  private ObservableBfCollection<BfItem, ObservableBfResource> _inventory;

  [ObservableProperty]
  private ObservableBfCollection<BfItem, ObservableBfResource> _key;

  [ObservableProperty]
  private ObservableBfCollection<BfItem, ObservableBfResource> _stored;

  public ObservableItemsSaveData(ItemsSaveData itemsSaveData) : base(itemsSaveData)
  {
    _itemsQuestsSaveData = itemsSaveData;
    _inventory = new(_itemsQuestsSaveData.Inventory,
      x => x.Select(bfItems => new ObservableBfResource(bfItems)).ToList());
    _key = new(_itemsQuestsSaveData.KeyItems,
      x => x.Select(bfItems => new ObservableBfResource(bfItems)).ToList());
    _stored = new(_itemsQuestsSaveData.Stored,
      x => x.Select(bfItems => new ObservableBfResource(bfItems)).ToList());
  }
}
