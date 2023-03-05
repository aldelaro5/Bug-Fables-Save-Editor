using System.Collections.ObjectModel;
using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using static BugFablesSaveEditor.BugFablesSave.Sections.Items;

namespace BugFablesSaveEditor.ViewModels;

public partial class ItemsViewModel : ObservableObject
{
  [ObservableProperty]
  private ObservableCollection<ItemInfo> _items = null!;

  [ObservableProperty]
  private string[] _itemsNames = null!;

  [ObservableProperty]
  private ObservableCollection<ItemInfo> _keyItems = null!;

  [ObservableProperty]
  private SaveData _saveData = null!;

  [ObservableProperty]
  [NotifyCanExecuteChangedFor(nameof(CmdReorderItemsUpCommand))]
  [NotifyCanExecuteChangedFor(nameof(CmdReorderItemsDownCommand))]
  private ItemInfo? _selectedItem;

  [ObservableProperty]
  private Item _selectedItemForAdd;

  [ObservableProperty]
  [NotifyCanExecuteChangedFor(nameof(CmdReorderKeyItemsUpCommand))]
  [NotifyCanExecuteChangedFor(nameof(CmdReorderKeyItemsDownCommand))]
  private ItemInfo? _selectedKeyItem;

  [ObservableProperty]
  private Item _selectedKeyItemForAdd;

  [ObservableProperty]
  [NotifyCanExecuteChangedFor(nameof(CmdReorderStoredItemsUpCommand))]
  [NotifyCanExecuteChangedFor(nameof(CmdReorderStoredItemsDownCommand))]
  private ItemInfo? _selectedStoredItem;

  [ObservableProperty]
  private Item _selectedStoredItemForAdd;

  [ObservableProperty]
  private ObservableCollection<ItemInfo> _storedItems = null!;

  public ItemsViewModel() : this(new SaveData())
  {
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
    ItemsNames = Common.GetEnumDescriptions<Item>();
    ObservableCollection<ItemInfo>[] itemsArray =
      (ObservableCollection<ItemInfo>[])SaveData.Sections[SaveFileSection.Items].Data;
    Items = itemsArray[(int)ItemPossessionType.Inventory];
    KeyItems = itemsArray[(int)ItemPossessionType.KeyItem];
    StoredItems = itemsArray[(int)ItemPossessionType.Stored];
  }

  [RelayCommand(CanExecute = nameof(CanReorderItemsUp))]
  private void CmdReorderItemsUp()
  {
    ReorderItem(ItemPossessionType.Inventory, ReorderDirection.Up);
  }

  private bool CanReorderItemsUp()
  {
    return Items.Count > 0 && SelectedItem is not null && Items[0] != SelectedItem;
  }

  [RelayCommand(CanExecute = nameof(CanReorderItemsDown))]
  private void CmdReorderItemsDown()
  {
    ReorderItem(ItemPossessionType.Inventory, ReorderDirection.Down);
  }

  private bool CanReorderItemsDown()
  {
    return Items.Count > 0 && SelectedItem is not null && Items[^1] != SelectedItem;
  }

  [RelayCommand(CanExecute = nameof(CanReorderKeyItemsUp))]
  private void CmdReorderKeyItemsUp()
  {
    ReorderItem(ItemPossessionType.KeyItem, ReorderDirection.Up);
  }

  private bool CanReorderKeyItemsUp()
  {
    return KeyItems.Count > 0 && SelectedKeyItem is not null && KeyItems[0] != SelectedKeyItem;
  }

  [RelayCommand(CanExecute = nameof(CanReorderKeyItemsDown))]
  private void CmdReorderKeyItemsDown()
  {
    ReorderItem(ItemPossessionType.KeyItem, ReorderDirection.Down);
  }

  private bool CanReorderKeyItemsDown()
  {
    return KeyItems.Count > 0 && SelectedKeyItem is not null && KeyItems[^1] != SelectedKeyItem;
  }

  [RelayCommand(CanExecute = nameof(CanReorderStoredItemsUp))]
  private void CmdReorderStoredItemsUp()
  {
    ReorderItem(ItemPossessionType.Stored, ReorderDirection.Up);
  }

  private bool CanReorderStoredItemsUp()
  {
    return StoredItems.Count > 0 && SelectedStoredItem is not null && StoredItems[0] != SelectedStoredItem;
  }

  [RelayCommand(CanExecute = nameof(CanReorderStoredItemsDown))]
  private void CmdReorderStoredItemsDown()
  {
    ReorderItem(ItemPossessionType.Stored, ReorderDirection.Down);
  }

  private bool CanReorderStoredItemsDown()
  {
    return StoredItems.Count > 0 && SelectedStoredItem is not null && StoredItems[^1] != SelectedStoredItem;
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
      newIndex--;
    else if (direction == ReorderDirection.Down)
      newIndex++;

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

  [RelayCommand]
  private void RemoveItem(ItemInfo itemInfo)
  {
    Items.Remove(itemInfo);
  }

  [RelayCommand]
  private void RemoveKeyItem(ItemInfo itemInfo)
  {
    KeyItems.Remove(itemInfo);
  }

  [RelayCommand]
  private void RemoveStoredItem(ItemInfo itemInfo)
  {
    StoredItems.Remove(itemInfo);
  }

  [RelayCommand]
  private void AddItem()
  {
    Items.Add(new ItemInfo { Item = SelectedItemForAdd });
  }

  [RelayCommand]
  private void AddKeyItem()
  {
    KeyItems.Add(new ItemInfo { Item = SelectedKeyItemForAdd });
  }

  [RelayCommand]
  private void AddStoredItem()
  {
    StoredItems.Add(new ItemInfo { Item = SelectedStoredItemForAdd });
  }
}
