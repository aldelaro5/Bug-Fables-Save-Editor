using BugFablesSaveEditor.BugFablesSave;
using BugFablesSaveEditor.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using static BugFablesSaveEditor.BugFablesSave.Sections.Items;

namespace BugFablesSaveEditor.ViewModels;

public partial class ItemsViewModel : ObservableObject
{
  [ObservableProperty]
  private ReorderableCollectionViewModel<ItemInfo> _itemsVm;

  [ObservableProperty]
  private ReorderableCollectionViewModel<ItemInfo> _keyItemsVm;

  [ObservableProperty]
  private ReorderableCollectionViewModel<ItemInfo> _storedItemsVm;

  [ObservableProperty]
  private string[] _itemsNames = Utils.GetEnumDescriptions<Item>();

  [ObservableProperty]
  private SaveData _saveData;

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
    _saveData = saveData;
    _itemsVm = new(_saveData.Items.Inventory);
    _keyItemsVm = new(_saveData.Items.Key);
    _storedItemsVm = new(_saveData.Items.Stored);
  }

  [RelayCommand]
  private void AddItem()
  {
    ItemsVm.Collection.Add(new ItemInfo { Item = SelectedItemForAdd });
    ItemsVm.CollectionView.Refresh();
  }

  [RelayCommand]
  private void AddKeyItem()
  {
    KeyItemsVm.Collection.Add(new ItemInfo { Item = SelectedKeyItemForAdd });
    KeyItemsVm.CollectionView.Refresh();
  }

  [RelayCommand]
  private void AddStoredItem()
  {
    StoredItemsVm.Collection.Add(new ItemInfo { Item = SelectedStoredItemForAdd });
    StoredItemsVm.CollectionView.Refresh();
  }
}
