using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Avalonia.Collections;
using BugFablesSaveEditor.BugFablesSave;
using BugFablesSaveEditor.BugFablesSave.Sections;
using BugFablesSaveEditor.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using static BugFablesSaveEditor.BugFablesSave.Sections.PartyMembers;
using static BugFablesSaveEditor.BugFablesSave.Sections.StatBonuses;

namespace BugFablesSaveEditor.ViewModels;

public partial class StatsViewModel : ObservableObject
{
  [ObservableProperty]
  private ObservableCollection<PartyMemberInfo> _partyMembers = new();

  [ObservableProperty]
  private SaveData _saveData = null!;

  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(TotalPartyMaxTPBonus))]
  [NotifyPropertyChangedFor(nameof(TotalPartyMaxMPBonus))]
  [NotifyPropertyChangedFor(nameof(TotalMemberMaxHPBonus))]
  [NotifyPropertyChangedFor(nameof(TotalMemberAttackBonus))]
  [NotifyPropertyChangedFor(nameof(TotalMemberDefenseBonus))]
  [NotifyCanExecuteChangedFor(nameof(AddMemberStatBonusCommand))]
  private PartyMemberInfo? _selectedMember;

  partial void OnSelectedMemberChanged(PartyMemberInfo value)
  {
    RefreshViews();
  }

  [ObservableProperty]
  private StatBonusType _statBonusTypeMemberSelectedForAdd;

  [ObservableProperty]
  private StatBonusType _statBonusTypePartySelectedForAdd;

  [ObservableProperty]
  private string[] _statBonusTypes = null!;
  [ObservableProperty]
  private int _statsBonusAmountMemberSelectedForAdd;
  [ObservableProperty]
  private int _statsBonusAmountPartySelectedForAdd;

  [ObservableProperty]
  private ObservableCollection<StatBonusInfo> _statsBonuses = new();

  [ObservableProperty]
  private DataGridCollectionView _viewMemberStatsBonuses = null!;

  [ObservableProperty]
  private DataGridCollectionView _viewPartyStatsBonuses = null!;
  private StatBonuses _statBonusesSection;

  public StatsViewModel() : this(new SaveData())
  {
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
    StatBonusTypes = Common.GetEnumDescriptions<StatBonusType>();
    _statBonusesSection = (StatBonuses)SaveData.Sections[SaveFileSection.StatBonuses];
    PartyMembers = (ObservableCollection<PartyMemberInfo>)SaveData.Sections[SaveFileSection.PartyMembers].Data;
    StatsBonuses = (ObservableCollection<StatBonusInfo>)SaveData.Sections[SaveFileSection.StatBonuses].Data;
    StatsBonuses.CollectionChanged += OnSaveStatsBonusesChanged;
    SetupViews();
  }

  [RelayCommand(CanExecute = nameof(CanRemovePartyStatBonus))]
  private void CmdRemovePartyStatBonus(StatBonusInfo info)
  {
    ViewPartyStatsBonuses.Remove(info);
    RefreshViews();
  }
  private bool CanRemovePartyStatBonus(StatBonusInfo info)
  {
    return !ViewPartyStatsBonuses.IsEditingItem;
  }

  [RelayCommand(CanExecute = nameof(CanRemoveMemberStatBonus))]
  private void CmdRemoveMemberStatBonus(StatBonusInfo info)
  {
    ViewMemberStatsBonuses.Remove(info);
    RefreshViews();
  }
  private bool CanRemoveMemberStatBonus(StatBonusInfo info)
  {
    return !ViewMemberStatsBonuses.IsEditingItem;
  }

  public int TotalPartyMaxTPBonus => _statBonusesSection.GetTotalBonusesForTargetAndType(-1, StatBonusType.TP);

  public int TotalPartyMaxMPBonus => _statBonusesSection.GetTotalBonusesForTargetAndType(-1, StatBonusType.MP);

  public int TotalMemberMaxHPBonus
  {
    get
    {
      if (SelectedMember == null)
        return 0;

      return _statBonusesSection.GetTotalBonusesForTargetAndType((int)SelectedMember.Trueid, StatBonusType.HP);
    }
  }

  public int TotalMemberAttackBonus
  {
    get
    {
      if (SelectedMember == null)
        return 0;

      return _statBonusesSection.GetTotalBonusesForTargetAndType((int)SelectedMember.Trueid, StatBonusType.Attack);
    }
  }

  public int TotalMemberDefenseBonus
  {
    get
    {
      if (SelectedMember == null)
        return 0;

      return _statBonusesSection.GetTotalBonusesForTargetAndType((int)SelectedMember.Trueid, StatBonusType.Defense);
    }
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

  [RelayCommand]
  private void AddPartyStatBonus()
  {
    StatBonusInfo statBonusInfo = new();
    statBonusInfo.Target = StatBonusTarget.Party;
    statBonusInfo.Type = StatBonusTypePartySelectedForAdd;
    statBonusInfo.Amount = StatsBonusAmountPartySelectedForAdd;
    StatsBonuses.Add(statBonusInfo);
    RefreshViews();
  }

  [RelayCommand(CanExecute = nameof(CanAddMemberStatBonus))]
  private void AddMemberStatBonus()
  {
    StatBonusInfo statBonusInfo = new();
    statBonusInfo.Target = (StatBonusTarget)(int)SelectedMember.Trueid;
    statBonusInfo.Type = StatBonusTypeMemberSelectedForAdd;
    statBonusInfo.Amount = StatsBonusAmountMemberSelectedForAdd;
    StatsBonuses.Add(statBonusInfo);
    RefreshViews();
  }

  private bool CanAddMemberStatBonus()
  {
    return SelectedMember is not null;
  }

  private void RefreshViews()
  {
    ViewMemberStatsBonuses.Refresh();
    ViewPartyStatsBonuses.Refresh();
  }

  private void OnSaveStatsBonusesChanged(object? sender, NotifyCollectionChangedEventArgs e)
  {
    OnPropertyChanged(nameof(TotalPartyMaxTPBonus));
    OnPropertyChanged(nameof(TotalPartyMaxMPBonus));
    OnPropertyChanged(nameof(TotalMemberMaxHPBonus));
    OnPropertyChanged(nameof(TotalMemberAttackBonus));
    OnPropertyChanged(nameof(TotalMemberDefenseBonus));
    RefreshViews();
  }
}
