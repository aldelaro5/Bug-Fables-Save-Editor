using System;
using BugFablesLib.Data;
using BugFablesLib.SaveData;
using BugFablesSaveEditor.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.Core.ViewModels;

public partial class ItemsViewModel : ObservableObject, IDisposable
{
  [ObservableProperty]
  private ViewModelCollection<BfItem, BfNamedIdModel> _inventory;

  [ObservableProperty]
  private ViewModelCollection<BfItem, BfNamedIdModel> _keyItems;

  [ObservableProperty]
  private ViewModelCollection<BfItem, BfNamedIdModel> _stored;

  public ItemsViewModel() : this(new()) { }

  public ItemsViewModel(ItemsSaveData itemsSaveData)
  {
    _inventory = new(itemsSaveData.Inventory);
    _keyItems = new(itemsSaveData.KeyItems);
    _stored = new(itemsSaveData.Stored);
  }

  public void Dispose()
  {
    Inventory.Dispose();
    KeyItems.Dispose();
    Stored.Dispose();
  }
}
