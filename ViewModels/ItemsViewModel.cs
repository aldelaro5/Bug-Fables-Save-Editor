using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using static BugFablesSaveEditor.BugFablesSave.Sections.Items;

namespace BugFablesSaveEditor.ViewModels
{
  public class ItemsViewModel : ViewModelBase
  {
    private SaveData _saveData;
    public SaveData SaveData
    {
      get { return _saveData; }
      set { _saveData = value; this.RaisePropertyChanged(); }
    }

    private string[] _itemsNames;
    public string[] ItemsNames
    {
      get { return _itemsNames; }
      set { _itemsNames = value; this.RaisePropertyChanged(); }
    }

    private Item _selectedItemForAdd;
    public Item SelectedItemForAdd
    {
      get { return _selectedItemForAdd; }
      set { _selectedItemForAdd = value; this.RaisePropertyChanged(); }
    }
    private Item _selectedKeyItemForAdd;
    public Item SelectedKeyItemForAdd
    {
      get { return _selectedKeyItemForAdd; }
      set { _selectedKeyItemForAdd = value; this.RaisePropertyChanged(); }
    }
    private Item _selectedStoredItemForAdd;
    public Item SelectedStoredItemForAdd
    {
      get { return _selectedStoredItemForAdd; }
      set { _selectedStoredItemForAdd = value; this.RaisePropertyChanged(); }
    }

    private ItemInfo _selectedItem;
    public ItemInfo SelectedItem
    {
      get { return _selectedItem; }
      set { _selectedItem = value; this.RaisePropertyChanged(); }
    }

    private ItemInfo _selectedKeyItem;
    public ItemInfo SelectedKeyItem
    {
      get { return _selectedKeyItem; }
      set { _selectedKeyItem = value; this.RaisePropertyChanged(); }
    }

    private ItemInfo _selectedStoredItem;
    public ItemInfo SelectedStoredItem
    {
      get { return _selectedStoredItem; }
      set { _selectedStoredItem = value; this.RaisePropertyChanged(); }
    }

    private ObservableCollection<ItemInfo> _items;
    public ObservableCollection<ItemInfo> Items
    {
      get { return _items; }
      set { _items = value; this.RaisePropertyChanged(); }
    }
    private ObservableCollection<ItemInfo> _keyItems;
    public ObservableCollection<ItemInfo> KeyItems
    {
      get { return _keyItems; }
      set { _keyItems = value; this.RaisePropertyChanged(); }
    }
    private ObservableCollection<ItemInfo> _storedItems;
    public ObservableCollection<ItemInfo> StoredItems
    {
      get { return _storedItems; }
      set { _storedItems = value; this.RaisePropertyChanged(); }
    }

    public ReactiveCommand<Unit, Unit> CmdReorderItemsUp { get; set; }
    public ReactiveCommand<Unit, Unit> CmdReorderItemsDown { get; set; }
    public ReactiveCommand<Unit, Unit> CmdReorderKeyItemsUp { get; set; }
    public ReactiveCommand<Unit, Unit> CmdReorderKeyItemsDown { get; set; }
    public ReactiveCommand<Unit, Unit> CmdReorderStoredItemsUp { get; set; }
    public ReactiveCommand<Unit, Unit> CmdReorderStoredItemsDown { get; set; }

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

    private void Initialize()
    {
      ItemsNames = Common.GetEnumDescriptions<Item>();
      var itemsArray = (ObservableCollection<ItemInfo>[])SaveData.Sections[SaveFileSection.Items].Data;
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
}
