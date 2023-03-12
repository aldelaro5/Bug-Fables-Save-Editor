using BugFablesSaveEditor.BugFablesSave;
using BugFablesSaveEditor.Enums;
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
    ItemsNames = Utils.GetEnumDescriptions<Item>();
    var itemsArray = SaveData.Items.List[(int)ItemPossessionType.Inventory];
    ItemsVm = new ReorderableCollectionViewModel<ItemInfo>(itemsArray.List);
    var keyItemsArray = SaveData.Items.List[(int)ItemPossessionType.KeyItem];
    var storedItemsArray = SaveData.Items.List[(int)ItemPossessionType.Stored];
    KeyItemsVm = new ReorderableCollectionViewModel<ItemInfo>(keyItemsArray.List);
    StoredItemsVm = new ReorderableCollectionViewModel<ItemInfo>(storedItemsArray.List);
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
