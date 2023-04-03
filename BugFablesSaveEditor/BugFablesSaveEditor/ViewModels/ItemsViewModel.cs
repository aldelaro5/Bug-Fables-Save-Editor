using BugFablesLib.Data;
using BugFablesSaveEditor.ObservableModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BugFablesSaveEditor.ViewModels;

public partial class ItemsViewModel : ObservableObject
{
  [ObservableProperty]
  private ObservableItemsSaveData _itemsSaveData;

  [ObservableProperty]
  private ObservableBfNamedId _newInventoryItem = new(new BfItem());

  [ObservableProperty]
  private ObservableBfNamedId _newKeyItem = new(new BfItem());

  [ObservableProperty]
  private ObservableBfNamedId _newStoredItem = new(new BfItem());

  public ItemsViewModel()
  {
    _itemsSaveData = new(new());
  }

  public ItemsViewModel(ObservableItemsSaveData itemsSaveData)
  {
    _itemsSaveData = itemsSaveData;
  }

  [RelayCommand]
  private void AddInventoryItem(ObservableBfNamedId item) =>
    ItemsSaveData.Inventory.Add(new(item.ToItem()));

  [RelayCommand]
  private void DeleteInventoryItem(ObservableBfNamedId item) =>
    ItemsSaveData.Inventory.Remove(item);

  [RelayCommand]
  private void AddKeyItem(ObservableBfNamedId item) =>
    ItemsSaveData.Key.Add(new(item.ToItem()));

  [RelayCommand]
  private void DeleteKeyItem(ObservableBfNamedId item) => ItemsSaveData.Key.Remove(item);

  [RelayCommand]
  private void AddStoredItem(ObservableBfNamedId item) =>
    ItemsSaveData.Stored.Add(new(item.ToItem()));

  [RelayCommand]
  private void DeleteStoredItem(ObservableBfNamedId item) =>
    ItemsSaveData.Stored.Remove(item);
}
