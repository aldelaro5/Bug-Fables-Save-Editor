using System;
using System.Collections.Specialized;
using System.Linq;
using BugFablesLib;
using BugFablesLib.Data;
using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

public partial class ObservableMedalShopsStockSaveData : ObservableObject
{
  private readonly MedalShopsStockSaveData _itemsQuestsSaveData;

  [ObservableProperty]
  private TrackedObservableCollection<ObservableBfResource> _merab;
  [ObservableProperty]
  private TrackedObservableCollection<ObservableBfResource> _shades;

  public ObservableMedalShopsStockSaveData(MedalShopsStockSaveData itemsSaveData)
  {
    _itemsQuestsSaveData = itemsSaveData;
    _merab = new(_itemsQuestsSaveData.Merab
      .Select(x => new ObservableBfResource(x)).ToList());
    _merab.CollectionChanged += MerabOnCollectionChanged;

    _shades = new(_itemsQuestsSaveData.Shades
      .Select(x => new ObservableBfResource(x)).ToList());
    _shades.CollectionChanged += ShadesOnCollectionChanged;
  }

  private void MerabOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) =>
    CollectionChanged(e, _itemsQuestsSaveData.Merab);

  private void ShadesOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) =>
    CollectionChanged(e, _itemsQuestsSaveData.Shades);

  private void CollectionChanged(NotifyCollectionChangedEventArgs e,
                                 BfSerializableCollection<BfMedal> collection)
  {
    var newList = e.NewItems?.Cast<ObservableBfResource>().ToList();
    switch (e.Action)
    {
      case NotifyCollectionChangedAction.Add:
        collection.Insert(e.NewStartingIndex, new BfMedal() { Id = newList![0].Id });
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
    Merab.CollectionChanged -= MerabOnCollectionChanged;
    Shades.CollectionChanged -= ShadesOnCollectionChanged;
  }
}
