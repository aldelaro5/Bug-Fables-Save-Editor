using BugFablesLib.Data;
using BugFablesLib.SaveData;
using BugFablesSaveEditor.ObservableModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

public partial class ItemsViewModel : ObservableObject
{
  [ObservableProperty]
  private ViewModelCollection<BfItem, ObservableBfNamedId> _inventory;

  [ObservableProperty]
  private ViewModelCollection<BfItem, ObservableBfNamedId> _keyItems;

  [ObservableProperty]
  private ViewModelCollection<BfItem, ObservableBfNamedId> _stored;

  public ItemsViewModel() : this(new()) { }

  public ItemsViewModel(ItemsSaveData itemsSaveData)
  {
    _inventory = new(itemsSaveData.Inventory);
    _keyItems = new(itemsSaveData.KeyItems);
    _stored = new(itemsSaveData.Stored);
  }
}
