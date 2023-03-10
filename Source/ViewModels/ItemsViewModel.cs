using System.Collections.Generic;
using BugFablesSaveEditor.BugFablesSave;
using BugFablesSaveEditor.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using static BugFablesSaveEditor.BugFablesSave.Sections.Items;

namespace BugFablesSaveEditor.ViewModels;

public partial class ItemsViewModel : ObservableObject
{
  [ObservableProperty]
  private ReorderableCollectionViewModel<ItemInfo> _itemsVm = null!;

  [ObservableProperty]
  private ReorderableCollectionViewModel<ItemInfo> _keyItemsVm = null!;

  [ObservableProperty]
  private ReorderableCollectionViewModel<ItemInfo> _storedItemsVm = null!;

  [ObservableProperty]
  private string[] _itemsNames = null!;

  [ObservableProperty]
  private SaveData _saveData = null!;

  [ObservableProperty]
  private Item _selectedItemForAdd;

  [ObservableProperty]
  private Item _selectedKeyItemForAdd;

  [ObservableProperty]
  private Item _selectedStoredItemForAdd;

  public ItemsViewModel() : this(new SaveData())
  {
    ItemsVm.Collection.Add(new ItemInfo { Item = (Item)61 });
    ItemsVm.Collection.Add(new ItemInfo { Item = (Item)165 });
    ItemsVm.Collection.Add(new ItemInfo { Item = (Item)43 });

    KeyItemsVm.Collection.Add(new ItemInfo { Item = (Item)62 });
    KeyItemsVm.Collection.Add(new ItemInfo { Item = (Item)7 });
    KeyItemsVm.Collection.Add(new ItemInfo { Item = (Item)28 });

    StoredItemsVm.Collection.Add(new ItemInfo { Item = (Item)12 });
    StoredItemsVm.Collection.Add(new ItemInfo { Item = (Item)25 });
    StoredItemsVm.Collection.Add(new ItemInfo { Item = (Item)14 });
  }

  public ItemsViewModel(SaveData saveData)
  {
    SaveData = saveData;
    ItemsNames = Common.GetEnumDescriptions<Item>();
    var itemsArray =
      (IEnumerable<ItemInfo>[])SaveData.Sections[SaveFileSection.Items].Data;
    ItemsVm = new ReorderableCollectionViewModel<ItemInfo>(
      itemsArray[(int)ItemPossessionType.Inventory]);
    KeyItemsVm =
      new ReorderableCollectionViewModel<ItemInfo>(itemsArray[(int)ItemPossessionType.KeyItem]);
    StoredItemsVm =
      new ReorderableCollectionViewModel<ItemInfo>(itemsArray[(int)ItemPossessionType.Stored]);
  }

  [RelayCommand]
  private void AddItem()
  {
    ItemsVm.Collection.Add(new ItemInfo { Item = SelectedItemForAdd });
  }

  [RelayCommand]
  private void AddKeyItem()
  {
    KeyItemsVm.Collection.Add(new ItemInfo { Item = SelectedKeyItemForAdd });
  }

  [RelayCommand]
  private void AddStoredItem()
  {
    StoredItemsVm.Collection.Add(new ItemInfo { Item = SelectedStoredItemForAdd });
  }
}
