using Avalonia.Collections;
using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using static BugFablesSaveEditor.BugFablesSave.Sections.Global;
using static BugFablesSaveEditor.BugFablesSave.Sections.PartyMembers;
using static BugFablesSaveEditor.BugFablesSave.Sections.StatBonuses;

namespace BugFablesSaveEditor.ViewModels
{
  public class StatsViewModel : ViewModelBase
  {
    private SaveData _saveData;
    public SaveData SaveData
    {
      get { return _saveData; }
      set { _saveData = value; this.RaisePropertyChanged(); }
    }

    private string[] _statBonusTypes;
    public string[] StatBonusTypes
    {
      get { return _statBonusTypes; }
      set { _statBonusTypes = value; this.RaisePropertyChanged(); }
    }

    private StatBonusType _statBonusTypePartySelectedForAdd;
    public StatBonusType StatBonusTypePartySelectedForAdd
    {
      get { return _statBonusTypePartySelectedForAdd; }
      set { _statBonusTypePartySelectedForAdd = value; this.RaisePropertyChanged(); }
    }
    private int _statsBonusAmountPartySelectedForAdd;
    public int StatsBonusAmountPartySelectedForAdd
    {
      get { return _statsBonusAmountPartySelectedForAdd; }
      set { _statsBonusAmountPartySelectedForAdd = value; this.RaisePropertyChanged(); }
    }

    private StatBonusType _statBonusTypeMemberSelectedForAdd;
    public StatBonusType StatBonusTypeMemberSelectedForAdd
    {
      get { return _statBonusTypeMemberSelectedForAdd; }
      set { _statBonusTypeMemberSelectedForAdd = value; this.RaisePropertyChanged(); }
    }
    private int _statsBonusAmountMemberSelectedForAdd;
    public int StatsBonusAmountMemberSelectedForAdd
    {
      get { return _statsBonusAmountMemberSelectedForAdd; }
      set { _statsBonusAmountMemberSelectedForAdd = value; this.RaisePropertyChanged(); }
    }

    private PartyMemberInfo _selectedMember;
    public PartyMemberInfo SelectedMember
    {
      get { return _selectedMember; }
      set
      {
        _selectedMember = value;
        this.RaisePropertyChanged();
        RaiseTotalBonusesChanged();
        RefreshViews();
      }
    }

    public ReactiveCommand<StatBonusInfo, Unit> CmdRemovePartyStatBonus { get; set; }
    public ReactiveCommand<StatBonusInfo, Unit> CmdRemoveMemberStatBonus { get; set; }

    private GlobalInfo saveGlobalInfo;
    private ObservableCollection<PartyMemberInfo> savePartyMembers;
    private ObservableCollection<StatBonusInfo> saveStatsBonuses;

    private ObservableCollection<PartyMemberInfo> _partyMembers = new ObservableCollection<PartyMemberInfo>();
    public ObservableCollection<PartyMemberInfo> PartyMembers
    {
      get { return _partyMembers; }
      set { _partyMembers = value; this.RaisePropertyChanged(); }
    }

    private ObservableCollection<StatBonusInfo> _statsBonuses = new ObservableCollection<StatBonusInfo>();
    public ObservableCollection<StatBonusInfo> StatsBonuses
    {
      get { return _statsBonuses; }
      set { _statsBonuses = value; this.RaisePropertyChanged(); }
    }

    private DataGridCollectionView _viewPartyStatsBonuses;
    public DataGridCollectionView ViewPartyStatsBonuses
    {
      get { return _viewPartyStatsBonuses; }
      set { _viewPartyStatsBonuses = value; this.RaisePropertyChanged(); }
    }

    private DataGridCollectionView _viewMemberStatsBonuses;
    public DataGridCollectionView ViewMemberStatsBonuses
    {
      get { return _viewMemberStatsBonuses; }
      set { _viewMemberStatsBonuses = value; this.RaisePropertyChanged(); }
    }

    public int TotalPartyMaxTPBonus
    {
      get
      {
        return saveStatsBonuses.Where(x => x.Target == StatBonusTarget.Party && x.Type == StatBonusType.TP)
                               .Sum(x => x.Amount);
      }
    }

    public int TotalPartyMaxMPBonus
    {
      get
      {
        return saveStatsBonuses.Where(x => x.Target == StatBonusTarget.Party && x.Type == StatBonusType.MP)
                               .Sum(x => x.Amount);
      }
    }

    public int TotalMemberMaxHPBonus
    {
      get
      {
        if (SelectedMember == null)
          return 0;

        return saveStatsBonuses.Where(x => ((int)x.Target == (int)SelectedMember.Trueid || x.Target == StatBonusTarget.Party) &&
                                            x.Type == StatBonusType.HP)
                               .Sum(x => x.Amount);
      }
    }

    public int TotalMemberAttackBonus
    {
      get
      {
        if (SelectedMember == null)
          return 0;

        return saveStatsBonuses.Where(x => ((int)x.Target == (int)SelectedMember.Trueid || x.Target == StatBonusTarget.Party) &&
                                            x.Type == StatBonusType.Attack)
                               .Sum(x => x.Amount);
      }
    }

    public int TotalMemberDefenseBonus
    {
      get
      {
        if (SelectedMember == null)
          return 0;

        return saveStatsBonuses.Where(x => ((int)x.Target == (int)SelectedMember.Trueid || x.Target == StatBonusTarget.Party) &&
                                            x.Type == StatBonusType.Defense)
                               .Sum(x => x.Amount);
      }
    }

    public StatsViewModel()
    {
      SaveData = new SaveData();
      Initialise();
      SetupViews();
      savePartyMembers.Add(new PartyMemberInfo { Index = 0, Trueid = AnimID.Bee });
      savePartyMembers.Add(new PartyMemberInfo { Index = 1, Trueid = AnimID.Beetle });
      savePartyMembers.Add(new PartyMemberInfo { Index = 2, Trueid = AnimID.Moth });

      saveStatsBonuses.Add(new StatBonusInfo { Type = StatBonusType.MP, Amount = 3, Target = StatBonusTarget.Party });
      saveStatsBonuses.Add(new StatBonusInfo { Type = StatBonusType.TP, Amount = 4, Target = StatBonusTarget.Party });
      saveStatsBonuses.Add(new StatBonusInfo { Type = StatBonusType.MP, Amount = 5, Target = StatBonusTarget.Party });
      saveStatsBonuses.Add(new StatBonusInfo { Type = StatBonusType.HP, Amount = 3, Target = StatBonusTarget.Vi });
      saveStatsBonuses.Add(new StatBonusInfo { Type = StatBonusType.Attack, Amount = 4, Target = StatBonusTarget.Kabbu });
      saveStatsBonuses.Add(new StatBonusInfo { Type = StatBonusType.Defense, Amount = 5, Target = StatBonusTarget.Leif });
    }

    public StatsViewModel(SaveData saveData)
    {
      SaveData = saveData;
      Initialise();
      SetupViews();
    }

    private void Initialise()
    {
      StatBonusTypes = Common.GetEnumDescriptions<StatBonusType>();
      saveGlobalInfo = (GlobalInfo)SaveData.Sections[SaveFileSection.Global].Data;
      savePartyMembers = (ObservableCollection<PartyMemberInfo>)SaveData.Sections[SaveFileSection.PartyMembers].Data;
      saveStatsBonuses = (ObservableCollection<StatBonusInfo>)SaveData.Sections[SaveFileSection.StatBonuses].Data;
      savePartyMembers.CollectionChanged += OnSaveDataPartyChanged;
      saveStatsBonuses.CollectionChanged += OnSaveStatsBonusesChanged;

      CmdRemovePartyStatBonus = ReactiveCommand.Create<StatBonusInfo>(x =>
      {
        ViewPartyStatsBonuses.Remove(x);
        RefreshViews();
      }, this.WhenAnyValue(x => x.ViewPartyStatsBonuses.IsEditingItem, x => !x));

      CmdRemoveMemberStatBonus = ReactiveCommand.Create<StatBonusInfo>(x =>
      {
        ViewMemberStatsBonuses.Remove(x);
        RefreshViews();
      }, this.WhenAnyValue(x => x.ViewMemberStatsBonuses.IsEditingItem, x => !x));

      saveGlobalInfo.PropertyChanged += GlobalInfoChanged;
    }

    private void SetupViews()
    {
      ViewPartyStatsBonuses = new DataGridCollectionView(saveStatsBonuses);
      ViewPartyStatsBonuses.Filter = FilterPartyStatsBonuses;
      ViewMemberStatsBonuses = new DataGridCollectionView(saveStatsBonuses);
      ViewMemberStatsBonuses.Filter = FilterMemberStatsBonuses;
    }

    private void GlobalInfoChanged(object? sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == nameof(saveGlobalInfo.MaxTP))
        this.RaisePropertyChanged(nameof(TotalPartyMaxTPBonus));
      if (e.PropertyName == nameof(saveGlobalInfo.MaxMP))
        this.RaisePropertyChanged(nameof(TotalPartyMaxMPBonus));
    }

    private bool FilterMemberStatsBonuses(object arg)
    {
      StatBonusInfo statBonusInfo = (StatBonusInfo)arg;
      if (SelectedMember == null)
        return false;
      return (int)statBonusInfo.Target == (int)SelectedMember.Trueid;
    }

    private bool FilterPartyStatsBonuses(object arg)
    {
      StatBonusInfo statBonusInfo = (StatBonusInfo)arg;
      return statBonusInfo.Target == StatBonusTarget.Party;
    }

    public void AddPartyStatBonus()
    {
      StatBonusInfo statBonusInfo = new StatBonusInfo();
      statBonusInfo.Target = StatBonusTarget.Party;
      statBonusInfo.Type = StatBonusTypePartySelectedForAdd;
      statBonusInfo.Amount = StatsBonusAmountPartySelectedForAdd;
      saveStatsBonuses.Add(statBonusInfo);
      RefreshViews();
    }

    public void AddMemberStatBonus()
    {
      StatBonusInfo statBonusInfo = new StatBonusInfo();
      statBonusInfo.Target = (StatBonusTarget)(int)SelectedMember.Trueid;
      statBonusInfo.Type = StatBonusTypeMemberSelectedForAdd;
      statBonusInfo.Amount = StatsBonusAmountMemberSelectedForAdd;
      saveStatsBonuses.Add(statBonusInfo);
      RefreshViews();
    }

    private void RefreshViews()
    {
      ViewMemberStatsBonuses.Refresh();
      ViewPartyStatsBonuses.Refresh();
    }

    private void OnSaveDataPartyChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
      if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems != null)
      {
        foreach (PartyMemberInfo item in e.NewItems)
          PartyMembers.Add(item);
      }
      else if (e.Action == NotifyCollectionChangedAction.Remove && e.OldItems != null)
      {
        foreach (PartyMemberInfo item in e.OldItems)
          PartyMembers.Remove(item);
      }
      else if (e.Action == NotifyCollectionChangedAction.Reset)
      {
        PartyMembers.Clear();

        if (e.NewItems != null)
        {
          foreach (PartyMemberInfo item in e.NewItems)
            PartyMembers.Add(item);
        }
      }

      RefreshViews();
    }

    private void OnSaveStatsBonusesChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
      if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems != null)
      {
        foreach (StatBonusInfo item in e.NewItems)
        {
          item.PropertyChanged += StatBonusChanged;
          StatsBonuses.Add(item);
        }
      }
      else if (e.Action == NotifyCollectionChangedAction.Remove && e.OldItems != null)
      {
        foreach (StatBonusInfo item in e.OldItems)
        {
          item.PropertyChanged -= StatBonusChanged;
          StatsBonuses.Remove(item);
        }
      }
      else if (e.Action == NotifyCollectionChangedAction.Reset)
      {
        foreach (var item in StatsBonuses)
          item.PropertyChanged -= StatBonusChanged;

        StatsBonuses.Clear();

        if (e.NewItems != null)
        {
          foreach (StatBonusInfo item in e.NewItems)
          {
            item.PropertyChanged += StatBonusChanged;
            StatsBonuses.Add(item);
          }
        }
      }

      RaiseTotalBonusesChanged();
      RefreshViews();
    }

    private void StatBonusChanged(object? sender, PropertyChangedEventArgs e)
    {
      RaiseTotalBonusesChanged();
    }

    private void RaiseTotalBonusesChanged()
    {
      this.RaisePropertyChanged(nameof(TotalPartyMaxTPBonus));
      this.RaisePropertyChanged(nameof(TotalPartyMaxMPBonus));
      this.RaisePropertyChanged(nameof(TotalMemberMaxHPBonus));
      this.RaisePropertyChanged(nameof(TotalMemberAttackBonus));
      this.RaisePropertyChanged(nameof(TotalMemberDefenseBonus));
    }
  }
}
