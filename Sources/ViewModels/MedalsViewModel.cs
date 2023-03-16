using BugFablesDataLib;
using BugFablesSaveEditor.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using static BugFablesDataLib.Sections.Medals;
using static BugFablesDataLib.Sections.MedalShopsAvailables;
using static BugFablesDataLib.Sections.MedalShopsPools;

namespace BugFablesSaveEditor.ViewModels;

public partial class MedalsViewModel : ObservableObject
{
  [ObservableProperty]
  private SaveData _saveData;

  [ObservableProperty]
  private ReorderableCollectionViewModel<MedalInfo> _medalsVm;

  [ObservableProperty]
  private ReorderableCollectionViewModel<MedalInShopAvailableInfo> _medalsMerabAvailablesVm;

  [ObservableProperty]
  private ReorderableCollectionViewModel<MedalInShopAvailableInfo> _medalsShadesAvailablesVm;

  [ObservableProperty]
  private ReorderableCollectionViewModel<MedalInShopPoolInfo> _medalsMerabPoolsVm;

  [ObservableProperty]
  private ReorderableCollectionViewModel<MedalInShopPoolInfo> _medalsShadesPoolsVm;

  [ObservableProperty]
  private string[] _medalsEquipTargetNames = Utils.GetEnumDescriptions<MedalEquipTarget>();

  [ObservableProperty]
  private string[] _medalsNames = Utils.GetEnumDescriptions<Medal>();

  [ObservableProperty]
  private Medal _selectedMedalForAdd;

  [ObservableProperty]
  private MedalEquipTarget _selectedMedalEquipTargetForAdd;

  [ObservableProperty]
  private Medal _selectedMedalMerabAvailableForAdd;

  [ObservableProperty]
  private Medal _selectedMedalMerabPoolForAdd;

  [ObservableProperty]
  private Medal _selectedMedalShadesAvailableForAdd;

  [ObservableProperty]
  private Medal _selectedMedalShadesPoolForAdd;

  public MedalsViewModel() : this(new SaveData())
  {
    MedalsVm.Collection.Add(new() { Medal = (Medal)7 });
    MedalsVm.Collection.Add(new() { Medal = (Medal)51 });
    MedalsVm.Collection.Add(new() { Medal = (Medal)78 });

    MedalsMerabPoolsVm.Collection.Add(new() { Medal = (Medal)62 });
    MedalsMerabPoolsVm.Collection.Add(new() { Medal = (Medal)51 });
    MedalsMerabPoolsVm.Collection.Add(new() { Medal = (Medal)78 });

    MedalsShadesPoolsVm.Collection.Add(new() { Medal = (Medal)35 });
    MedalsShadesPoolsVm.Collection.Add(new() { Medal = (Medal)52 });
    MedalsShadesPoolsVm.Collection.Add(new() { Medal = (Medal)13 });

    MedalsMerabAvailablesVm.Collection.Add(new() { Medal = (Medal)12 });
    MedalsMerabAvailablesVm.Collection.Add(new() { Medal = (Medal)25 });
    MedalsMerabAvailablesVm.Collection.Add(new() { Medal = (Medal)14 });

    MedalsShadesAvailablesVm.Collection.Add(new() { Medal = (Medal)9 });
    MedalsShadesAvailablesVm.Collection.Add(new() { Medal = (Medal)4 });
    MedalsShadesAvailablesVm.Collection.Add(new() { Medal = (Medal)55 });
  }

  public MedalsViewModel(SaveData saveData)
  {
    _saveData = saveData;
    _medalsVm = new(_saveData.Medals.List);

    _medalsMerabPoolsVm = new(_saveData.MedalShopsPools.Merab);
    _medalsMerabAvailablesVm = new(_saveData.MedalShopsAvailables.Merab);
    _medalsShadesPoolsVm = new(_saveData.MedalShopsPools.Shades);
    _medalsShadesAvailablesVm = new(_saveData.MedalShopsAvailables.Shades);
  }

  [RelayCommand]
  private void AddMedal()
  {
    MedalInfo? item = new();
    item.Medal = SelectedMedalForAdd;
    item.MedalEquipTarget = SelectedMedalEquipTargetForAdd;
    MedalsVm.Collection.Add(item);
    MedalsVm.CollectionView.Refresh();
  }

  [RelayCommand]
  private void AddMedalMerabPool()
  {
    MedalInShopPoolInfo? item = new();
    item.Medal = SelectedMedalMerabPoolForAdd;
    MedalsMerabPoolsVm.Collection.Add(item);
    MedalsMerabPoolsVm.CollectionView.Refresh();
  }

  [RelayCommand]
  private void AddMedalMerabAvailable()
  {
    MedalInShopAvailableInfo? item = new();
    item.Medal = SelectedMedalMerabAvailableForAdd;
    MedalsMerabAvailablesVm.Collection.Add(item);
    MedalsMerabAvailablesVm.CollectionView.Refresh();
  }

  [RelayCommand]
  private void AddMedalShadesPool()
  {
    MedalInShopPoolInfo? item = new();
    item.Medal = SelectedMedalShadesPoolForAdd;
    MedalsShadesPoolsVm.Collection.Add(item);
    MedalsShadesPoolsVm.CollectionView.Refresh();
  }

  [RelayCommand]
  private void AddMedalShadesAvailable()
  {
    MedalInShopAvailableInfo? item = new();
    item.Medal = SelectedMedalShadesAvailableForAdd;
    MedalsShadesAvailablesVm.Collection.Add(item);
    MedalsShadesAvailablesVm.CollectionView.Refresh();
  }

  [RelayCommand]
  private void UnequipAllMedals()
  {
    foreach (MedalInfo medal in MedalsVm.Collection)
      medal.MedalEquipTarget = MedalEquipTarget.Unequipped;
    MedalsVm.CollectionView.Refresh();
  }
}
