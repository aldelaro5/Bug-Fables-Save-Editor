using BugFablesLib.Data;
using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ObservableModels;

public partial class ObservableItemsSaveData : ObservableObject
{
  public ItemsSaveData Model { get; }

  [ObservableProperty]
  private ViewModelCollection<BfItem, ObservableBfNamedId> _inventory;

  [ObservableProperty]
  private ViewModelCollection<BfItem, ObservableBfNamedId> _key;

  [ObservableProperty]
  private ViewModelCollection<BfItem, ObservableBfNamedId> _stored;

  public ObservableItemsSaveData(ItemsSaveData itemsSaveData)
  {
    Model = itemsSaveData;
    _inventory = new(Model.Inventory, x => new ObservableBfNamedId(x));
    _key = new(Model.KeyItems, x => new ObservableBfNamedId(x));
    _stored = new(Model.Stored, x => new ObservableBfNamedId(x));
  }
}
