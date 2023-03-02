using System.Collections.ObjectModel;
using System.Reactive;
using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave;
using ReactiveUI;
using static BugFablesSaveEditor.BugFablesSave.Sections.Items;

namespace BugFablesSaveEditor.ViewModels;

public class ItemsViewModel : ViewModelBase
{
  private ObservableCollection<ItemInfo> _items;

  private string[] _itemsNames;
  private ObservableCollection<ItemInfo> _keyItems;
  private SaveData _saveData;

  private ItemInfo _selectedItem;

  private Item _selectedItemForAdd;

  private ItemInfo _selectedKeyItem;
  private Item _selectedKeyItemForAdd;

  private ItemInfo _selectedStoredItem;
  private Item _selectedStoredItemForAdd;
  private ObservableCollection<ItemInfo> _storedItems;

  public ItemsViewModel()
  {
    SaveData = new SaveData();
    Initialize();

    Items.Add(new ItemInfo { Item = (Item)61 });
    Items.Add(new ItemInfo { Item = (Item)165 });
    Items.Add(new ItemInfo { Item = (Item)43 });

    KeyItems.Add(new ItemInfo { Item = (Item)62 });
    KeyItems.Add(new ItemInfo { Item = (Item)7 });
    KeyItems.Add(new ItemInfo { Item = (Item)28 });

    StoredItems.Add(new ItemInfo { Item = (Item)12 });
    StoredItems.Add(new ItemInfo { Item = (Item)25 });
    StoredItems.Add(new ItemInfo { Item = (Item)14 });
  }

  public ItemsViewModel(SaveData saveData)
  {
    SaveData = saveData;
    Initialize();
  }

  public SaveData SaveData
  {
    get => _saveData;
    set
    {
      _saveData = value;
      this.RaisePropertyChanged();
    }
  }

  public string[] ItemsNames
  {
    get => _itemsNames;
    set
    {
      _itemsNames = value;
      this.RaisePropertyChanged();
    }
  }

  public Item SelectedItemForAdd
  {
    get => _selectedItemForAdd;
    set
    {
      _selectedItemForAdd = value;
      this.RaisePropertyChanged();
    }
  }

  public Item SelectedKeyItemForAdd
  {
    get => _selectedKeyItemForAdd;
    set
    {
      _selectedKeyItemForAdd = value;
      this.RaisePropertyChanged();
    }
  }

  public Item SelectedStoredItemForAdd
  {
    get => _selectedStoredItemForAdd;
    set
    {
      _selectedStoredItemForAdd = value;
      this.RaisePropertyChanged();
    }
  }

  public ItemInfo SelectedItem
  {
    get => _selectedItem;
    set
    {
      _selectedItem = value;
      this.RaisePropertyChanged();
    }
  }

  public ItemInfo SelectedKeyItem
  {
    get => _selectedKeyItem;
    set
    {
      _selectedKeyItem = value;
      this.RaisePropertyChanged();
    }
  }

  public ItemInfo SelectedStoredItem
  {
    get => _selectedStoredItem;
    set
    {
      _selectedStoredItem = value;
      this.RaisePropertyChanged();
    }
  }

  public ObservableCollection<ItemInfo> Items
  {
    get => _items;
    set
    {
      _items = value;
      this.RaisePropertyChanged();
    }
  }

  public ObservableCollection<ItemInfo> KeyItems
  {
    get => _keyItems;
    set
    {
      _keyItems = value;
      this.RaisePropertyChanged();
    }
  }

  public ObservableCollection<ItemInfo> StoredItems
  {
    get => _storedItems;
    set
    {
      _storedItems = value;
      this.RaisePropertyChanged();
    }
  }

  public ReactiveCommand<Unit, Unit> CmdReorderItemsUp { get; set; }
  public ReactiveCommand<Unit, Unit> CmdReorderItemsDown { get; set; }
  public ReactiveCommand<Unit, Unit> CmdReorderKeyItemsUp { get; set; }
  public ReactiveCommand<Unit, Unit> CmdReorderKeyItemsDown { get; set; }
  public ReactiveCommand<Unit, Unit> CmdReorderStoredItemsUp { get; set; }
  public ReactiveCommand<Unit, Unit> CmdReorderStoredItemsDown { get; set; }

  private void Initialize()
  {
    ItemsNames = Common.GetEnumDescriptions<Item>();
    ObservableCollection<ItemInfo>[] itemsArray =
      (ObservableCollection<ItemInfo>[])SaveData.Sections[SaveFileSection.Items].Data;
    Items = itemsArray[(int)ItemPossessionType.Inventory];
    KeyItems = itemsArray[(int)ItemPossessionType.KeyItem];
    StoredItems = itemsArray[(int)ItemPossessionType.Stored];

    CmdReorderItemsUp = ReactiveCommand.Create(() =>
    {
      ReorderItem(ItemPossessionType.Inventory, ReorderDirection.Up);
    }, this.WhenAnyValue(x => x.SelectedItem, x => x != null && Items[0] != x));
    CmdReorderItemsDown = ReactiveCommand.Create(() =>
    {
      ReorderItem(ItemPossessionType.Inventory, ReorderDirection.Down);
    }, this.WhenAnyValue(x => x.SelectedItem, x => x != null && Items[Items.Count - 1] != x));

    CmdReorderKeyItemsUp = ReactiveCommand.Create(() =>
    {
      ReorderItem(ItemPossessionType.KeyItem, ReorderDirection.Up);
    }, this.WhenAnyValue(x => x.SelectedKeyItem, x => x != null && KeyItems[0] != x));
    CmdReorderKeyItemsDown = ReactiveCommand.Create(() =>
    {
      ReorderItem(ItemPossessionType.KeyItem, ReorderDirection.Down);
    }, this.WhenAnyValue(x => x.SelectedKeyItem, x => x != null && KeyItems[KeyItems.Count - 1] != x));

    CmdReorderStoredItemsUp = ReactiveCommand.Create(() =>
    {
      ReorderItem(ItemPossessionType.Stored, ReorderDirection.Up);
    }, this.WhenAnyValue(x => x.SelectedStoredItem, x => x != null && StoredItems[0] != x));
    CmdReorderStoredItemsDown = ReactiveCommand.Create(() =>
    {
      ReorderItem(ItemPossessionType.Stored, ReorderDirection.Down);
    }, this.WhenAnyValue(x => x.SelectedStoredItem, x => x != null && StoredItems[StoredItems.Count - 1] != x));
  }

  private void ReorderItem(ItemPossessionType possesionType, ReorderDirection direction)
  {
    ItemInfo selectedItem;
    ObservableCollection<ItemInfo> itemsCollection;
    switch (possesionType)
    {
      case ItemPossessionType.Inventory:
        selectedItem = SelectedItem;
        itemsCollection = Items;
        break;
      case ItemPossessionType.KeyItem:
        selectedItem = SelectedKeyItem;
        itemsCollection = KeyItems;
        break;
      case ItemPossessionType.Stored:
        selectedItem = SelectedStoredItem;
        itemsCollection = StoredItems;
        break;
      default:
        return;
    }

    int index = itemsCollection.IndexOf(selectedItem);
    int newIndex = index;
    if (direction == ReorderDirection.Up)
    {
      newIndex--;
    }
    else if (direction == ReorderDirection.Down)
    {
      newIndex++;
    }

    Item item = selectedItem.Item;
    itemsCollection.Remove(selectedItem);
    itemsCollection.Insert(newIndex, new ItemInfo { Item = item });

    switch (possesionType)
    {
      case ItemPossessionType.Inventory:
        SelectedItem = Items[newIndex];
        break;
      case ItemPossessionType.KeyItem:
        SelectedKeyItem = KeyItems[newIndex];
        break;
      case ItemPossessionType.Stored:
        SelectedStoredItem = StoredItems[newIndex];
        break;
    }
  }

  public void RemoveItem(ItemInfo itemInfo)
  {
    Items.Remove(itemInfo);
  }

  public void RemoveKeyItem(ItemInfo itemInfo)
  {
    KeyItems.Remove(itemInfo);
  }

  public void RemoveStoredItem(ItemInfo itemInfo)
  {
    StoredItems.Remove(itemInfo);
  }

  public void AddItem()
  {
    Items.Add(new ItemInfo { Item = SelectedItemForAdd });
  }

  public void AddKeyItem()
  {
    KeyItems.Add(new ItemInfo { Item = SelectedKeyItemForAdd });
  }

  public void AddStoredItem()
  {
    StoredItems.Add(new ItemInfo { Item = SelectedStoredItemForAdd });
  }
}
