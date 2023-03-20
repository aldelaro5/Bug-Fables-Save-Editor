using System;
using System.Collections.Specialized;
using System.Linq;
using BugFablesLib;
using BugFablesLib.Data;
using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

public partial class ObservableItemsSaveData : ObservableObject
{
  private readonly ItemsSaveData _itemsQuestsSaveData;

  [ObservableProperty]
  private TrackedObservableCollection<ObservableBfResource> _inventory;
  [ObservableProperty]
  private TrackedObservableCollection<ObservableBfResource> _key;
  [ObservableProperty]
  private TrackedObservableCollection<ObservableBfResource> _stored;

  public ObservableItemsSaveData(ItemsSaveData itemsSaveData)
  {
    _itemsQuestsSaveData = itemsSaveData;
    _inventory = new TrackedObservableCollection<ObservableBfResource>(_itemsQuestsSaveData.Inventory
      .Select(x => new ObservableBfResource(x)).ToList());
    _inventory.CollectionChanged += InventoryOnCollectionChanged;

    _key = new TrackedObservableCollection<ObservableBfResource>(_itemsQuestsSaveData.Key
      .Select(x => new ObservableBfResource(x)).ToList());
    _key.CollectionChanged += KeyOnCollectionChanged;

    _stored = new TrackedObservableCollection<ObservableBfResource>(_itemsQuestsSaveData.Stored
      .Select(x => new ObservableBfResource(x)).ToList());
    _stored.CollectionChanged += StoredOnCollectionChanged;
  }

  private void InventoryOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) =>
    CollectionChanged(e, _itemsQuestsSaveData.Inventory);

  private void KeyOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) =>
    CollectionChanged(e, _itemsQuestsSaveData.Key);

  private void StoredOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) =>
    CollectionChanged(e, _itemsQuestsSaveData.Stored);

  private void CollectionChanged(NotifyCollectionChangedEventArgs e,
                                 BfSerializableCollection<BfItem> collection)
  {
    var newList = e.NewItems?.Cast<ObservableBfResource>().ToList();
    switch (e.Action)
    {
      case NotifyCollectionChangedAction.Add:
        collection.Insert(e.NewStartingIndex, new BfItem() { Id = newList![0].Id });
        break;
      case NotifyCollectionChangedAction.Remove:
        collection.RemoveAt(e.OldStartingIndex);
        break;
      case NotifyCollectionChangedAction.Replace:
        break;
      case NotifyCollectionChangedAction.Move:
        break;
      case NotifyCollectionChangedAction.Reset:
        break;
      default:
        throw new ArgumentOutOfRangeException();
    }
  }

  public void Dispose()
  {
    Inventory.CollectionChanged -= InventoryOnCollectionChanged;
    Key.CollectionChanged -= KeyOnCollectionChanged;
    Stored.CollectionChanged -= StoredOnCollectionChanged;
  }
}
