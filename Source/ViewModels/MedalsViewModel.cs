using System.Collections.Generic;
using BugFablesSaveEditor.BugFablesSave;
using BugFablesSaveEditor.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using static BugFablesSaveEditor.BugFablesSave.Sections.Medals;
using static BugFablesSaveEditor.BugFablesSave.Sections.MedalShopsAvailables;
using static BugFablesSaveEditor.BugFablesSave.Sections.MedalShopsPools;

namespace BugFablesSaveEditor.ViewModels;

public partial class MedalsViewModel : ObservableObject
{
  [ObservableProperty]
  private ReorderableCollectionViewModel<MedalInfo> _medalsVm = null!;

  [ObservableProperty]
  private ReorderableCollectionViewModel<MedalShopAvailable> _medalsMerabAvailablesVm = null!;

  [ObservableProperty]
  private ReorderableCollectionViewModel<MedalShopAvailable> _medalsShadesAvailablesVm = null!;

  [ObservableProperty]
  private ReorderableCollectionViewModel<MedalShopPool> _medalsMerabPoolsVm = null!;

  [ObservableProperty]
  private ReorderableCollectionViewModel<MedalShopPool> _medalsShadesPoolsVm = null!;

  [ObservableProperty]
  private string[] _medalsEquipTargetNames = null!;

  [ObservableProperty]
  private string[] _medalsNames = null!;

  [ObservableProperty]
  private SaveData _saveData = null!;

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
    MedalsVm.Collection.Add(new MedalInfo { Medal = (Medal)7 });
    MedalsVm.Collection.Add(new MedalInfo { Medal = (Medal)51 });
    MedalsVm.Collection.Add(new MedalInfo { Medal = (Medal)78 });

    MedalsMerabPoolsVm.Collection.Add(new MedalShopPool { Medal = (Medal)62 });
    MedalsMerabPoolsVm.Collection.Add(new MedalShopPool { Medal = (Medal)51 });
    MedalsMerabPoolsVm.Collection.Add(new MedalShopPool { Medal = (Medal)78 });

    MedalsShadesPoolsVm.Collection.Add(new MedalShopPool { Medal = (Medal)35 });
    MedalsShadesPoolsVm.Collection.Add(new MedalShopPool { Medal = (Medal)52 });
    MedalsShadesPoolsVm.Collection.Add(new MedalShopPool { Medal = (Medal)13 });

    MedalsMerabAvailablesVm.Collection.Add(new MedalShopAvailable { Medal = (Medal)12 });
    MedalsMerabAvailablesVm.Collection.Add(new MedalShopAvailable { Medal = (Medal)25 });
    MedalsMerabAvailablesVm.Collection.Add(new MedalShopAvailable { Medal = (Medal)14 });

    MedalsShadesAvailablesVm.Collection.Add(new MedalShopAvailable { Medal = (Medal)9 });
    MedalsShadesAvailablesVm.Collection.Add(new MedalShopAvailable { Medal = (Medal)4 });
    MedalsShadesAvailablesVm.Collection.Add(new MedalShopAvailable { Medal = (Medal)55 });
  }

  public MedalsViewModel(SaveData saveData)
  {
    SaveData = saveData;
    MedalsNames = Common.GetEnumDescriptions<Medal>();
    MedalsEquipTargetNames = Common.GetEnumDescriptions<MedalEquipTarget>();
    MedalsVm =
      new ReorderableCollectionViewModel<MedalInfo>(
        (IEnumerable<MedalInfo>)SaveData.Sections[SaveFileSection.Medals].Data);

    var medalsPoolArray =
      (IEnumerable<MedalShopPool>[])SaveData.Sections[SaveFileSection.MedalShopsPools].Data;
    var medalsAvailableArray =
      (IEnumerable<MedalShopAvailable>[])SaveData.Sections[SaveFileSection.MedalShopsAvailables]
        .Data;

    MedalsMerabPoolsVm =
      new ReorderableCollectionViewModel<MedalShopPool>(medalsPoolArray[(int)MedalShop.Merab]);
    MedalsMerabAvailablesVm =
      new ReorderableCollectionViewModel<MedalShopAvailable>(
        medalsAvailableArray[(int)MedalShop.Merab]);
    MedalsShadesPoolsVm =
      new ReorderableCollectionViewModel<MedalShopPool>(medalsPoolArray[(int)MedalShop.Shades]);
    MedalsShadesAvailablesVm =
      new ReorderableCollectionViewModel<MedalShopAvailable>(
        medalsAvailableArray[(int)MedalShop.Shades]);
  }

  [RelayCommand]
  private void AddMedal()
  {
    MedalsVm.Collection.Add(new MedalInfo
    {
      Medal = SelectedMedalForAdd, MedalEquipTarget = SelectedMedalEquipTargetForAdd
    });
  }

  [RelayCommand]
  private void AddMedalMerabPool()
  {
    MedalsMerabPoolsVm.Collection.Add(new MedalShopPool { Medal = SelectedMedalMerabPoolForAdd });
  }

  [RelayCommand]
  private void AddMedalMerabAvailable()
  {
    MedalsMerabAvailablesVm.Collection.Add(
      new MedalShopAvailable { Medal = SelectedMedalMerabAvailableForAdd });
  }

  [RelayCommand]
  private void AddMedalShadesPool()
  {
    MedalsShadesPoolsVm.Collection.Add(new MedalShopPool { Medal = SelectedMedalShadesPoolForAdd });
  }

  [RelayCommand]
  private void AddMedalShadesAvailable()
  {
    MedalsShadesAvailablesVm.Collection.Add(
      new MedalShopAvailable { Medal = SelectedMedalShadesAvailableForAdd });
  }

  [RelayCommand]
  private void UnequipAllMedals()
  {
    foreach (MedalInfo medal in MedalsVm.Collection)
      medal.MedalEquipTarget = MedalEquipTarget.Unequipped;
  }
}
