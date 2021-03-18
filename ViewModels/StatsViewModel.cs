using Avalonia.Collections;
using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave;
using BugFablesSaveEditor.BugFablesSave.Sections;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reactive;
using static BugFablesSaveEditor.BugFablesSave.Sections.PartyMembers;
using static BugFablesSaveEditor.BugFablesSave.Sections.StatBonuses;

namespace BugFablesSaveEditor.ViewModels
{
  public class StatsViewModel : ViewModelBase
  {
    private StatBonuses statBonusesSection;

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
        return statBonusesSection.GetTotalBonusesForTargetAndType(-1, StatBonusType.TP);
      }
    }

    public int TotalPartyMaxMPBonus
    {
      get
      {
        return statBonusesSection.GetTotalBonusesForTargetAndType(-1, StatBonusType.MP);
      }
    }

    public int TotalMemberMaxHPBonus
    {
      get
      {
        if (SelectedMember == null)
          return 0;

        return statBonusesSection.GetTotalBonusesForTargetAndType((int)SelectedMember.Trueid, StatBonusType.HP);
      }
    }

    public int TotalMemberAttackBonus
    {
      get
      {
        if (SelectedMember == null)
          return 0;

        return statBonusesSection.GetTotalBonusesForTargetAndType((int)SelectedMember.Trueid, StatBonusType.Attack);
      }
    }

    public int TotalMemberDefenseBonus
    {
      get
      {
        if (SelectedMember == null)
          return 0;

        return statBonusesSection.GetTotalBonusesForTargetAndType((int)SelectedMember.Trueid, StatBonusType.Defense);
      }
    }

    public StatsViewModel()
    {
      SaveData = new SaveData();
      Initialise();
      SetupViews();
      PartyMembers.Add(new PartyMemberInfo { Trueid = AnimID.Bee });
      PartyMembers.Add(new PartyMemberInfo { Trueid = AnimID.Beetle });
      PartyMembers.Add(new PartyMemberInfo { Trueid = AnimID.Moth });

      StatsBonuses.Add(new StatBonusInfo { Type = StatBonusType.MP, Amount = 3, Target = StatBonusTarget.Party });
      StatsBonuses.Add(new StatBonusInfo { Type = StatBonusType.TP, Amount = 4, Target = StatBonusTarget.Party });
      StatsBonuses.Add(new StatBonusInfo { Type = StatBonusType.MP, Amount = 5, Target = StatBonusTarget.Party });
      StatsBonuses.Add(new StatBonusInfo { Type = StatBonusType.HP, Amount = 3, Target = StatBonusTarget.Vi });
      StatsBonuses.Add(new StatBonusInfo { Type = StatBonusType.Attack, Amount = 4, Target = StatBonusTarget.Kabbu });
      StatsBonuses.Add(new StatBonusInfo { Type = StatBonusType.Defense, Amount = 5, Target = StatBonusTarget.Leif });
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
      statBonusesSection = (StatBonuses)SaveData.Sections[SaveFileSection.StatBonuses];
      PartyMembers = (ObservableCollection<PartyMemberInfo>)SaveData.Sections[SaveFileSection.PartyMembers].Data;
      StatsBonuses = (ObservableCollection<StatBonusInfo>)SaveData.Sections[SaveFileSection.StatBonuses].Data;
      StatsBonuses.CollectionChanged += OnSaveStatsBonusesChanged;

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
    }

    private void SetupViews()
    {
      ViewPartyStatsBonuses = new DataGridCollectionView(StatsBonuses);
      ViewPartyStatsBonuses.Filter = FilterPartyStatsBonuses;
      ViewMemberStatsBonuses = new DataGridCollectionView(StatsBonuses);
      ViewMemberStatsBonuses.Filter = FilterMemberStatsBonuses;
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
      StatsBonuses.Add(statBonusInfo);
      RefreshViews();
    }

    public void AddMemberStatBonus()
    {
      StatBonusInfo statBonusInfo = new StatBonusInfo();
      statBonusInfo.Target = (StatBonusTarget)(int)SelectedMember.Trueid;
      statBonusInfo.Type = StatBonusTypeMemberSelectedForAdd;
      statBonusInfo.Amount = StatsBonusAmountMemberSelectedForAdd;
      StatsBonuses.Add(statBonusInfo);
      RefreshViews();
    }

    private void RefreshViews()
    {
      ViewMemberStatsBonuses.Refresh();
      ViewPartyStatsBonuses.Refresh();
    }

    private void OnSaveStatsBonusesChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
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
