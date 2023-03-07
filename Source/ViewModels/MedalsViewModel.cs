using System.Collections;
using System.Collections.ObjectModel;
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
  public enum MedalsDataType
  {
    Medals,
    MerabPool,
    MerabAvailable,
    ShadesPool,
    ShadesAvailable
  }

  [ObservableProperty]
  private ObservableCollection<MedalInfo> _medals = null!;

  [ObservableProperty]
  private string[] _medalsEquipTargetNames = null!;

  [ObservableProperty]
  private ObservableCollection<MedalShopAvailable> _MedalsMerabAvailables = null!;

  [ObservableProperty]
  private ObservableCollection<MedalShopPool> _medalsMerabPools = null!;

  [ObservableProperty]
  private string[] _medalsNames = null!;

  [ObservableProperty]
  private ObservableCollection<MedalShopAvailable> _MedalsShadesAvailables = null!;

  [ObservableProperty]
  private ObservableCollection<MedalShopPool> _medalsShadesPools = null!;

  [ObservableProperty]
  private SaveData _saveData = null!;

  [ObservableProperty]
  [NotifyCanExecuteChangedFor(nameof(CmdReorderMedalsUpCommand))]
  [NotifyCanExecuteChangedFor(nameof(CmdReorderMedalsDownCommand))]
  private MedalInfo? _selectedMedal;

  [ObservableProperty]
  private Medal _selectedMedalForAdd;

  [ObservableProperty]
  [NotifyCanExecuteChangedFor(nameof(CmdReorderMedalsMerabAvailablesUpCommand))]
  [NotifyCanExecuteChangedFor(nameof(CmdReorderMedalsMerabAvailablesDownCommand))]
  private MedalShopAvailable? _selectedMedalMerabAvailable;

  [ObservableProperty]
  private Medal _selectedMedalMerabAvailableForAdd;

  [ObservableProperty]
  [NotifyCanExecuteChangedFor(nameof(CmdReorderMedalsMerabPoolsUpCommand))]
  [NotifyCanExecuteChangedFor(nameof(CmdReorderMedalsMerabPoolsDownCommand))]
  private MedalShopPool? _selectedMedalMerabPool;

  [ObservableProperty]
  private Medal _selectedMedalMerabPoolForAdd;

  [ObservableProperty]
  [NotifyCanExecuteChangedFor(nameof(CmdReorderMedalsShadesAvailablesUpCommand))]
  [NotifyCanExecuteChangedFor(nameof(CmdReorderMedalsShadesAvailablesDownCommand))]
  private MedalShopAvailable? _selectedMedalShadesAvailable;

  [ObservableProperty]
  private Medal _selectedMedalShadesAvailableForAdd;

  [ObservableProperty]
  [NotifyCanExecuteChangedFor(nameof(CmdReorderMedalsShadesPoolsUpCommand))]
  [NotifyCanExecuteChangedFor(nameof(CmdReorderMedalsShadesPoolsDownCommand))]
  private MedalShopPool? _selectedMedalShadesPool;

  [ObservableProperty]
  private Medal _selectedMedalShadesPoolForAdd;

  public MedalsViewModel() : this(new SaveData())
  {
    Medals.Add(new MedalInfo { Medal = (Medal)7 });
    Medals.Add(new MedalInfo { Medal = (Medal)51 });
    Medals.Add(new MedalInfo { Medal = (Medal)78 });

    MedalsMerabPools.Add(new MedalShopPool { Medal = (Medal)62 });
    MedalsMerabPools.Add(new MedalShopPool { Medal = (Medal)51 });
    MedalsMerabPools.Add(new MedalShopPool { Medal = (Medal)78 });

    MedalsShadesPools.Add(new MedalShopPool { Medal = (Medal)35 });
    MedalsShadesPools.Add(new MedalShopPool { Medal = (Medal)52 });
    MedalsShadesPools.Add(new MedalShopPool { Medal = (Medal)13 });

    MedalsMerabAvailables.Add(new MedalShopAvailable { Medal = (Medal)12 });
    MedalsMerabAvailables.Add(new MedalShopAvailable { Medal = (Medal)25 });
    MedalsMerabAvailables.Add(new MedalShopAvailable { Medal = (Medal)14 });

    MedalsShadesAvailables.Add(new MedalShopAvailable { Medal = (Medal)9 });
    MedalsShadesAvailables.Add(new MedalShopAvailable { Medal = (Medal)4 });
    MedalsShadesAvailables.Add(new MedalShopAvailable { Medal = (Medal)55 });
  }

  public MedalsViewModel(SaveData saveData)
  {
    SaveData = saveData;
    MedalsNames = Common.GetEnumDescriptions<Medal>();
    MedalsEquipTargetNames = Common.GetEnumDescriptions<MedalEquipTarget>();
    Medals = (ObservableCollection<MedalInfo>)SaveData.Sections[SaveFileSection.Medals].Data;
    ObservableCollection<MedalShopPool>[] medalsPoolArray =
      (ObservableCollection<MedalShopPool>[])SaveData.Sections[SaveFileSection.MedalShopsPools]
        .Data;
    ObservableCollection<MedalShopAvailable>[] medalsAvailableArray =
      (ObservableCollection<MedalShopAvailable>[])SaveData
        .Sections[SaveFileSection.MedalShopsAvailables].Data;
    MedalsMerabPools = medalsPoolArray[(int)MedalShop.Merab];
    MedalsMerabAvailables = medalsAvailableArray[(int)MedalShop.Merab];
    MedalsShadesPools = medalsPoolArray[(int)MedalShop.Shades];
    MedalsShadesAvailables = medalsAvailableArray[(int)MedalShop.Shades];
  }

  public MedalEquipTarget SelectedMedalEquipTargetForAdd { get; set; }

  [RelayCommand(CanExecute = nameof(CanReorderMedalsUp))]
  private void CmdReorderMedalsUp()
  {
    ReorderMedal(MedalsDataType.Medals, ReorderDirection.Up);
  }

  private bool CanReorderMedalsUp()
  {
    return Medals.Count > 0 && SelectedMedal is not null && Medals[0] != SelectedMedal;
  }

  [RelayCommand(CanExecute = nameof(CanReorderMedalsDown))]
  private void CmdReorderMedalsDown()
  {
    ReorderMedal(MedalsDataType.Medals, ReorderDirection.Down);
  }

  private bool CanReorderMedalsDown()
  {
    return Medals.Count > 0 && SelectedMedal is not null && Medals[^1] != SelectedMedal;
  }

  [RelayCommand(CanExecute = nameof(CanReorderMedalsMerabPoolsUp))]
  private void CmdReorderMedalsMerabPoolsUp()
  {
    ReorderMedal(MedalsDataType.MerabPool, ReorderDirection.Up);
  }

  private bool CanReorderMedalsMerabPoolsUp()
  {
    return MedalsMerabPools.Count > 0 && SelectedMedalMerabPool is not null &&
           MedalsMerabPools[0] != SelectedMedalMerabPool;
  }

  [RelayCommand(CanExecute = nameof(CanReorderMedalsMerabPoolsDown))]
  private void CmdReorderMedalsMerabPoolsDown()
  {
    ReorderMedal(MedalsDataType.MerabPool, ReorderDirection.Down);
  }

  private bool CanReorderMedalsMerabPoolsDown()
  {
    return MedalsMerabPools.Count > 0 && SelectedMedalMerabPool is not null &&
           MedalsMerabPools[^1] != SelectedMedalMerabPool;
  }

  [RelayCommand(CanExecute = nameof(CanReorderMedalsMerabAvailablesUp))]
  private void CmdReorderMedalsMerabAvailablesUp()
  {
    ReorderMedal(MedalsDataType.MerabAvailable, ReorderDirection.Up);
  }

  private bool CanReorderMedalsMerabAvailablesUp()
  {
    return MedalsMerabAvailables.Count > 0 && SelectedMedalMerabAvailable is not null &&
           MedalsMerabAvailables[0] != SelectedMedalMerabAvailable;
  }

  [RelayCommand(CanExecute = nameof(CanReorderMedalsMerabAvailablesDown))]
  private void CmdReorderMedalsMerabAvailablesDown()
  {
    ReorderMedal(MedalsDataType.MerabAvailable, ReorderDirection.Down);
  }

  private bool CanReorderMedalsMerabAvailablesDown()
  {
    return MedalsMerabAvailables.Count > 0 && SelectedMedalMerabAvailable is not null &&
           MedalsMerabAvailables[^1] != SelectedMedalMerabAvailable;
  }

  [RelayCommand(CanExecute = nameof(CanReorderMedalsShadesPoolsUp))]
  private void CmdReorderMedalsShadesPoolsUp()
  {
    ReorderMedal(MedalsDataType.ShadesPool, ReorderDirection.Up);
  }

  private bool CanReorderMedalsShadesPoolsUp()
  {
    return MedalsShadesPools.Count > 0 && SelectedMedalShadesPool is not null &&
           MedalsShadesPools[0] != SelectedMedalShadesPool;
  }

  [RelayCommand(CanExecute = nameof(CanReorderMedalsShadesPoolsDown))]
  private void CmdReorderMedalsShadesPoolsDown()
  {
    ReorderMedal(MedalsDataType.ShadesPool, ReorderDirection.Down);
  }

  private bool CanReorderMedalsShadesPoolsDown()
  {
    return MedalsShadesPools.Count > 0 && SelectedMedalShadesPool is not null &&
           MedalsShadesPools[^1] != SelectedMedalShadesPool;
  }

  [RelayCommand(CanExecute = nameof(CanReorderMedalsShadesAvailablesUp))]
  private void CmdReorderMedalsShadesAvailablesUp()
  {
    ReorderMedal(MedalsDataType.ShadesAvailable, ReorderDirection.Up);
  }

  private bool CanReorderMedalsShadesAvailablesUp()
  {
    return MedalsShadesAvailables.Count > 0 && SelectedMedalShadesAvailable is not null &&
           MedalsShadesAvailables[0] != SelectedMedalShadesAvailable;
  }

  [RelayCommand(CanExecute = nameof(CanReorderMedalsShadesAvailablesDown))]
  private void CmdReorderMedalsShadesAvailablesDown()
  {
    ReorderMedal(MedalsDataType.ShadesAvailable, ReorderDirection.Down);
  }

  private bool CanReorderMedalsShadesAvailablesDown()
  {
    return MedalsShadesAvailables.Count > 0 && SelectedMedalShadesAvailable is not null &&
           MedalsShadesAvailables[^1] != SelectedMedalShadesAvailable;
  }

  private void ReorderMedal(MedalsDataType dataType, ReorderDirection direction)
  {
    object? selectedItem;
    Medal? medal;
    MedalEquipTarget? medalEquipTarget = MedalEquipTarget.Unequipped;
    IList itemsCollection;
    switch (dataType)
    {
      case MedalsDataType.Medals:
        selectedItem = SelectedMedal;
        medal = ((MedalInfo?)selectedItem)?.Medal;
        medalEquipTarget = ((MedalInfo?)selectedItem)?.MedalEquipTarget;
        itemsCollection = Medals;
        break;
      case MedalsDataType.MerabPool:
        selectedItem = SelectedMedalMerabPool;
        medal = ((MedalShopPool?)selectedItem)?.Medal;
        itemsCollection = MedalsMerabPools;
        break;
      case MedalsDataType.MerabAvailable:
        selectedItem = SelectedMedalMerabAvailable;
        medal = ((MedalShopAvailable?)selectedItem)?.Medal;
        itemsCollection = MedalsMerabAvailables;
        break;
      case MedalsDataType.ShadesPool:
        selectedItem = SelectedMedalShadesPool;
        medal = ((MedalShopPool?)selectedItem)?.Medal;
        itemsCollection = MedalsShadesPools;
        break;
      case MedalsDataType.ShadesAvailable:
        selectedItem = SelectedMedalShadesAvailable;
        medal = ((MedalShopAvailable?)selectedItem)?.Medal;
        itemsCollection = MedalsShadesAvailables;
        break;
      default:
        return;
    }

    if (medal is null || medalEquipTarget is null)
      return;

    int index = itemsCollection.IndexOf(selectedItem);
    int newIndex = index;
    if (direction == ReorderDirection.Up)
      newIndex--;
    else if (direction == ReorderDirection.Down)
      newIndex++;

    itemsCollection.Remove(selectedItem);

    switch (dataType)
    {
      case MedalsDataType.Medals:
        itemsCollection.Insert(newIndex,
          new MedalInfo { Medal = medal.Value, MedalEquipTarget = medalEquipTarget.Value });
        SelectedMedal = Medals[newIndex];
        break;
      case MedalsDataType.MerabPool:
        itemsCollection.Insert(newIndex, new MedalShopPool { Medal = medal.Value });
        SelectedMedalMerabPool = MedalsMerabPools[newIndex];
        break;
      case MedalsDataType.MerabAvailable:
        itemsCollection.Insert(newIndex, new MedalShopAvailable { Medal = medal.Value });
        SelectedMedalMerabAvailable = MedalsMerabAvailables[newIndex];
        break;
      case MedalsDataType.ShadesPool:
        itemsCollection.Insert(newIndex, new MedalShopPool { Medal = medal.Value });
        SelectedMedalShadesPool = MedalsShadesPools[newIndex];
        break;
      case MedalsDataType.ShadesAvailable:
        itemsCollection.Insert(newIndex, new MedalShopAvailable { Medal = medal.Value });
        SelectedMedalShadesAvailable = MedalsShadesAvailables[newIndex];
        break;
    }
  }

  [RelayCommand]
  private void RemoveMedal(MedalInfo medalInfo)
  {
    Medals.Remove(medalInfo);
  }

  [RelayCommand]
  private void RemoveMedalMerabPool(MedalShopPool medalInfo)
  {
    MedalsMerabPools.Remove(medalInfo);
  }

  [RelayCommand]
  private void RemoveMedalMerabAvailable(MedalShopAvailable medalInfo)
  {
    MedalsMerabAvailables.Remove(medalInfo);
  }

  [RelayCommand]
  private void RemoveMedalShadesPool(MedalShopPool medalInfo)
  {
    MedalsShadesPools.Remove(medalInfo);
  }

  [RelayCommand]
  private void RemoveMedalShadesAvailable(MedalShopAvailable medalInfo)
  {
    MedalsShadesAvailables.Remove(medalInfo);
  }

  [RelayCommand]
  private void AddMedal()
  {
    Medals.Add(new MedalInfo
    {
      Medal = SelectedMedalForAdd, MedalEquipTarget = SelectedMedalEquipTargetForAdd
    });
  }

  [RelayCommand]
  private void AddMedalMerabPool()
  {
    MedalsMerabPools.Add(new MedalShopPool { Medal = SelectedMedalMerabPoolForAdd });
  }

  [RelayCommand]
  private void AddMedalMerabAvailable()
  {
    MedalsMerabAvailables.Add(new MedalShopAvailable { Medal = SelectedMedalMerabAvailableForAdd });
  }

  [RelayCommand]
  private void AddMedalShadesPool()
  {
    MedalsShadesPools.Add(new MedalShopPool { Medal = SelectedMedalShadesPoolForAdd });
  }

  [RelayCommand]
  private void AddMedalShadesAvailable()
  {
    MedalsShadesAvailables.Add(
      new MedalShopAvailable { Medal = SelectedMedalShadesAvailableForAdd });
  }

  [RelayCommand]
  private void UnequipAllMedals()
  {
    foreach (MedalInfo medal in Medals)
    {
      medal.MedalEquipTarget = MedalEquipTarget.Unequipped;
    }
  }
}
