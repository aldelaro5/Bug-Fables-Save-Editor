using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave;
using ReactiveUI;
using System.Collections;
using System.Collections.ObjectModel;
using System.Reactive;
using static BugFablesSaveEditor.BugFablesSave.Sections.Medals;
using static BugFablesSaveEditor.BugFablesSave.Sections.MedalShopsAvailables;
using static BugFablesSaveEditor.BugFablesSave.Sections.MedalShopsPools;

namespace BugFablesSaveEditor.ViewModels
{
  public class MedalsViewModel : ViewModelBase
  {
    public enum MedalsDataType
    {
      Medals,
      MerabPool,
      MerabAvailable,
      ShadesPool,
      ShadesAvailable
    }

    private SaveData _saveData;
    public SaveData SaveData
    {
      get { return _saveData; }
      set { _saveData = value; this.RaisePropertyChanged(); }
    }

    private string[] _medalsNames;
    public string[] MedalsNames
    {
      get { return _medalsNames; }
      set { _medalsNames = value; this.RaisePropertyChanged(); }
    }
    private string[] _medalsEquipTargetNames;
    public string[] MedalsEquipTargetNames
    {
      get { return _medalsEquipTargetNames; }
      set { _medalsEquipTargetNames = value; this.RaisePropertyChanged(); }
    }

    private Medal _selectedMedalForAdd;
    public Medal SelectedMedalForAdd
    {
      get { return _selectedMedalForAdd; }
      set { _selectedMedalForAdd = value; this.RaisePropertyChanged(); }
    }
    private MedalEquipTarget _selectedMedalEquipTargetForAdd;
    public MedalEquipTarget SelectedMedalEquipTargetForAdd
    {
      get { return _selectedMedalEquipTargetForAdd; }
      set { _selectedMedalEquipTargetForAdd = value; }
    }
    private Medal _selectedMedalMerabPoolForAdd;
    public Medal SelectedMedalMerabPoolForAdd
    {
      get { return _selectedMedalMerabPoolForAdd; }
      set { _selectedMedalMerabPoolForAdd = value; this.RaisePropertyChanged(); }
    }
    private Medal _selectedMedalMerabAvailableForAdd;
    public Medal SelectedMedalMerabAvailableForAdd
    {
      get { return _selectedMedalMerabAvailableForAdd; }
      set { _selectedMedalMerabAvailableForAdd = value; this.RaisePropertyChanged(); }
    }
    private Medal _selectedMedalShadesPoolForAdd;
    public Medal SelectedMedalShadesPoolForAdd
    {
      get { return _selectedMedalShadesPoolForAdd; }
      set { _selectedMedalShadesPoolForAdd = value; this.RaisePropertyChanged(); }
    }
    private Medal _selectedMedalShadesAvailableForAdd;
    public Medal SelectedMedalShadesAvailableForAdd
    {
      get { return _selectedMedalShadesAvailableForAdd; }
      set { _selectedMedalShadesAvailableForAdd = value; this.RaisePropertyChanged(); }
    }

    private MedalInfo _selectedMedal;
    public MedalInfo SelectedMedal
    {
      get { return _selectedMedal; }
      set { _selectedMedal = value; this.RaisePropertyChanged(); }
    }
    private MedalShopPool _selectedMedalMerabPool;
    public MedalShopPool SelectedMedalMerabPool
    {
      get { return _selectedMedalMerabPool; }
      set { _selectedMedalMerabPool = value; this.RaisePropertyChanged(); }
    }
    private MedalShopAvailable _selectedMedalMerabAvailable;
    public MedalShopAvailable SelectedMedalMerabAvailable
    {
      get { return _selectedMedalMerabAvailable; }
      set { _selectedMedalMerabAvailable = value; this.RaisePropertyChanged(); }
    }
    private MedalShopPool _selectedMedalShadesPool;
    public MedalShopPool SelectedMedalShadesPool
    {
      get { return _selectedMedalShadesPool; }
      set { _selectedMedalShadesPool = value; this.RaisePropertyChanged(); }
    }
    private MedalShopAvailable _selectedMedalShadesAvailable;
    public MedalShopAvailable SelectedMedalShadesAvailable
    {
      get { return _selectedMedalShadesAvailable; }
      set { _selectedMedalShadesAvailable = value; this.RaisePropertyChanged(); }
    }

    private ObservableCollection<MedalInfo> _medals;
    public ObservableCollection<MedalInfo> Medals
    {
      get { return _medals; }
      set { _medals = value; this.RaisePropertyChanged(); }
    }
    private ObservableCollection<MedalShopPool> _medalsMerabPools;
    public ObservableCollection<MedalShopPool> MedalsMerabPools
    {
      get { return _medalsMerabPools; }
      set { _medalsMerabPools = value; this.RaisePropertyChanged(); }
    }
    private ObservableCollection<MedalShopAvailable> _MedalsMerabAvailables;
    public ObservableCollection<MedalShopAvailable> MedalsMerabAvailables
    {
      get { return _MedalsMerabAvailables; }
      set { _MedalsMerabAvailables = value; this.RaisePropertyChanged(); }
    }
    private ObservableCollection<MedalShopPool> _medalsShadesPools;
    public ObservableCollection<MedalShopPool> MedalsShadesPools
    {
      get { return _medalsShadesPools; }
      set { _medalsShadesPools = value; this.RaisePropertyChanged(); }
    }
    private ObservableCollection<MedalShopAvailable> _MedalsShadesAvailables;
    public ObservableCollection<MedalShopAvailable> MedalsShadesAvailables
    {
      get { return _MedalsShadesAvailables; }
      set { _MedalsShadesAvailables = value; this.RaisePropertyChanged(); }
    }

    public ReactiveCommand<Unit, Unit> CmdReorderMedalsUp { get; set; }
    public ReactiveCommand<Unit, Unit> CmdReorderMedalsDown { get; set; }
    public ReactiveCommand<Unit, Unit> CmdReorderMedalsMerabPoolsUp { get; set; }
    public ReactiveCommand<Unit, Unit> CmdReorderMedalsMerabPoolsDown { get; set; }
    public ReactiveCommand<Unit, Unit> CmdReorderMedalsMerabAvailablesUp { get; set; }
    public ReactiveCommand<Unit, Unit> CmdReorderMedalsMerabAvailablesDown { get; set; }
    public ReactiveCommand<Unit, Unit> CmdReorderMedalsShadesPoolsUp { get; set; }
    public ReactiveCommand<Unit, Unit> CmdReorderMedalsShadesPoolsDown { get; set; }
    public ReactiveCommand<Unit, Unit> CmdReorderMedalsShadesAvailablesUp { get; set; }
    public ReactiveCommand<Unit, Unit> CmdReorderMedalsShadesAvailablesDown { get; set; }

    public MedalsViewModel()
    {
      SaveData = new SaveData();
      Initialize();

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
      Initialize();
    }

    private void Initialize()
    {
      MedalsNames = Common.GetEnumDescriptions<Medal>();
      MedalsEquipTargetNames = Common.GetEnumDescriptions<MedalEquipTarget>();
      Medals = (ObservableCollection<MedalInfo>)SaveData.Sections[SaveFileSection.Medals].Data;
      var medalsPoolArray = (ObservableCollection<MedalShopPool>[])SaveData.Sections[SaveFileSection.MedalShopsPools].Data;
      var medalsAvailableArray = (ObservableCollection<MedalShopAvailable>[])SaveData.Sections[SaveFileSection.MedalShopsAvailables].Data;
      MedalsMerabPools = medalsPoolArray[(int)MedalShop.Merab];
      MedalsMerabAvailables = medalsAvailableArray[(int)MedalShop.Merab];
      MedalsShadesPools = medalsPoolArray[(int)MedalShop.Shades];
      MedalsShadesAvailables = medalsAvailableArray[(int)MedalShop.Shades];

      CmdReorderMedalsUp = ReactiveCommand.Create(() =>
      {
        ReorderMedal(MedalsDataType.Medals, ReorderDirection.Up);
      }, this.WhenAnyValue(x => x.SelectedMedal, x => x != null && Medals[0] != x));
      CmdReorderMedalsDown = ReactiveCommand.Create(() =>
      {
        ReorderMedal(MedalsDataType.Medals, ReorderDirection.Down);
      }, this.WhenAnyValue(x => x.SelectedMedal, x => x != null && Medals[Medals.Count - 1] != x));

      CmdReorderMedalsMerabPoolsUp = ReactiveCommand.Create(() =>
      {
        ReorderMedal(MedalsDataType.MerabPool, ReorderDirection.Up);
      }, this.WhenAnyValue(x => x.SelectedMedalMerabPool, x => x != null && MedalsMerabPools[0] != x));
      CmdReorderMedalsMerabPoolsDown = ReactiveCommand.Create(() =>
      {
        ReorderMedal(MedalsDataType.MerabPool, ReorderDirection.Down);
      }, this.WhenAnyValue(x => x.SelectedMedalMerabPool, x => x != null && MedalsMerabPools[MedalsMerabPools.Count - 1] != x));

      CmdReorderMedalsMerabAvailablesUp = ReactiveCommand.Create(() =>
      {
        ReorderMedal(MedalsDataType.MerabAvailable, ReorderDirection.Up);
      }, this.WhenAnyValue(x => x.SelectedMedalMerabAvailable, x => x != null && MedalsMerabAvailables[0] != x));
      CmdReorderMedalsMerabAvailablesDown = ReactiveCommand.Create(() =>
      {
        ReorderMedal(MedalsDataType.MerabAvailable, ReorderDirection.Down);
      }, this.WhenAnyValue(x => x.SelectedMedalMerabAvailable, x => x != null && MedalsMerabAvailables[MedalsMerabAvailables.Count - 1] != x));

      CmdReorderMedalsShadesPoolsUp = ReactiveCommand.Create(() =>
      {
        ReorderMedal(MedalsDataType.ShadesPool, ReorderDirection.Up);
      }, this.WhenAnyValue(x => x.SelectedMedalShadesPool, x => x != null && MedalsShadesPools[0] != x));
      CmdReorderMedalsShadesPoolsDown = ReactiveCommand.Create(() =>
      {
        ReorderMedal(MedalsDataType.ShadesPool, ReorderDirection.Down);
      }, this.WhenAnyValue(x => x.SelectedMedalShadesPool, x => x != null && MedalsShadesPools[MedalsShadesPools.Count - 1] != x));

      CmdReorderMedalsShadesAvailablesUp = ReactiveCommand.Create(() =>
      {
        ReorderMedal(MedalsDataType.ShadesAvailable, ReorderDirection.Up);
      }, this.WhenAnyValue(x => x.SelectedMedalShadesAvailable, x => x != null && MedalsShadesAvailables[0] != x));
      CmdReorderMedalsShadesAvailablesDown = ReactiveCommand.Create(() =>
      {
        ReorderMedal(MedalsDataType.ShadesAvailable, ReorderDirection.Down);
      }, this.WhenAnyValue(x => x.SelectedMedalShadesAvailable, x => x != null && MedalsShadesAvailables[MedalsShadesAvailables.Count - 1] != x));
    }

    private void ReorderMedal(MedalsDataType dataType, ReorderDirection direction)
    {
      object selectedItem;
      Medal medal;
      MedalEquipTarget medalEquipTarget = MedalEquipTarget.Unequipped;
      IList itemsCollection;
      switch (dataType)
      {
        case MedalsDataType.Medals:
          selectedItem = SelectedMedal;
          medal = ((MedalInfo)selectedItem).Medal;
          medalEquipTarget = ((MedalInfo)selectedItem).MedalEquipTarget;
          itemsCollection = Medals;
          break;
        case MedalsDataType.MerabPool:
          selectedItem = SelectedMedalMerabPool;
          medal = ((MedalShopPool)selectedItem).Medal;
          itemsCollection = MedalsMerabPools;
          break;
        case MedalsDataType.MerabAvailable:
          selectedItem = SelectedMedalMerabAvailable;
          medal = ((MedalShopAvailable)selectedItem).Medal;
          itemsCollection = MedalsMerabAvailables;
          break;
        case MedalsDataType.ShadesPool:
          selectedItem = SelectedMedalShadesPool;
          medal = ((MedalShopPool)selectedItem).Medal;
          itemsCollection = MedalsShadesPools;
          break;
        case MedalsDataType.ShadesAvailable:
          selectedItem = SelectedMedalShadesAvailable;
          medal = ((MedalShopAvailable)selectedItem).Medal;
          itemsCollection = MedalsShadesAvailables;
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
      itemsCollection.Remove(selectedItem);

      switch (dataType)
      {
        case MedalsDataType.Medals:
          itemsCollection.Insert(newIndex, new MedalInfo { Medal = medal, MedalEquipTarget = medalEquipTarget });
          SelectedMedal = Medals[newIndex];
          break;
        case MedalsDataType.MerabPool:
          itemsCollection.Insert(newIndex, new MedalShopPool { Medal = medal });
          SelectedMedalMerabPool = MedalsMerabPools[newIndex];
          break;
        case MedalsDataType.MerabAvailable:
          itemsCollection.Insert(newIndex, new MedalShopAvailable { Medal = medal });
          SelectedMedalMerabAvailable = MedalsMerabAvailables[newIndex];
          break;
        case MedalsDataType.ShadesPool:
          itemsCollection.Insert(newIndex, new MedalShopPool { Medal = medal });
          SelectedMedalShadesPool = MedalsShadesPools[newIndex];
          break;
        case MedalsDataType.ShadesAvailable:
          itemsCollection.Insert(newIndex, new MedalShopAvailable { Medal = medal });
          SelectedMedalShadesAvailable = MedalsShadesAvailables[newIndex];
          break;
      }
    }

    public void RemoveMedal(MedalInfo medalInfo)
    {
      Medals.Remove(medalInfo);
    }

    public void RemoveMedalMerabPool(MedalShopPool medalInfo)
    {
      MedalsMerabPools.Remove(medalInfo);
    }

    public void RemoveMedalMerabAvailable(MedalShopAvailable medalInfo)
    {
      MedalsMerabAvailables.Remove(medalInfo);
    }

    public void RemoveMedalShadesPool(MedalShopPool medalInfo)
    {
      MedalsShadesPools.Remove(medalInfo);
    }

    public void RemoveMedalShadesAvailable(MedalShopAvailable medalInfo)
    {
      MedalsShadesAvailables.Remove(medalInfo);
    }

    public void AddMedal()
    {
      Medals.Add(new MedalInfo { Medal = SelectedMedalForAdd, MedalEquipTarget = SelectedMedalEquipTargetForAdd });
    }

    public void AddMedalMerabPool()
    {
      MedalsMerabPools.Add(new MedalShopPool { Medal = SelectedMedalMerabPoolForAdd });
    }

    public void AddMedalMerabAvailable()
    {
      MedalsMerabAvailables.Add(new MedalShopAvailable { Medal = SelectedMedalMerabAvailableForAdd });
    }

    public void AddMedalShadesPool()
    {
      MedalsShadesPools.Add(new MedalShopPool { Medal = SelectedMedalShadesPoolForAdd });
    }

    public void AddMedalShadesAvailable()
    {
      MedalsShadesAvailables.Add(new MedalShopAvailable { Medal = SelectedMedalShadesAvailableForAdd });
    }
  }
}
