using System.Collections.Generic;
using Avalonia.Collections;
using BugFablesSaveEditor.BugFablesSave;
using BugFablesSaveEditor.BugFablesSave.Sections;
using BugFablesSaveEditor.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using static BugFablesSaveEditor.BugFablesSave.Sections.PartyMembers;
using static BugFablesSaveEditor.BugFablesSave.Sections.StatBonuses;

namespace BugFablesSaveEditor.ViewModels;

public partial class StatsViewModel : ObservableObject
{
  [ObservableProperty]
  private IList<PartyMemberInfo> _partyMembers;

  [ObservableProperty]
  private SaveData _saveData = null!;

  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(TotalPartyMaxTpBonus))]
  [NotifyPropertyChangedFor(nameof(TotalPartyMaxMpBonus))]
  [NotifyPropertyChangedFor(nameof(TotalMemberMaxHpBonus))]
  [NotifyPropertyChangedFor(nameof(TotalMemberAttackBonus))]
  [NotifyPropertyChangedFor(nameof(TotalMemberDefenseBonus))]
  [NotifyCanExecuteChangedFor(nameof(AddMemberStatBonusCommand))]
  private PartyMemberInfo? _selectedMember;

  partial void OnSelectedMemberChanged(PartyMemberInfo? value)
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
  private IList<StatBonusInfo> _statsBonuses;

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

    StatsBonuses.Add(new StatBonusInfo
    {
      Type = StatBonusType.MP, Amount = 3, Target = StatBonusTarget.Party
    });
    StatsBonuses.Add(new StatBonusInfo
    {
      Type = StatBonusType.TP, Amount = 4, Target = StatBonusTarget.Party
    });
    StatsBonuses.Add(new StatBonusInfo
    {
      Type = StatBonusType.MP, Amount = 5, Target = StatBonusTarget.Party
    });
    StatsBonuses.Add(new StatBonusInfo
    {
      Type = StatBonusType.HP, Amount = 3, Target = StatBonusTarget.Vi
    });
    StatsBonuses.Add(new StatBonusInfo
    {
      Type = StatBonusType.Attack, Amount = 4, Target = StatBonusTarget.Kabbu
    });
    StatsBonuses.Add(new StatBonusInfo
    {
      Type = StatBonusType.Defense, Amount = 5, Target = StatBonusTarget.Leif
    });
  }

  public StatsViewModel(SaveData saveData)
  {
    SaveData = saveData;
    StatBonusTypes = Utils.GetEnumDescriptions<StatBonusType>();
    _statBonusesSection = SaveData.StatBonuses;
    _partyMembers = SaveData.PartyMembers.List;
    _statsBonuses = SaveData.StatBonuses.List;
    ViewPartyStatsBonuses = new DataGridCollectionView(StatsBonuses);
    ViewPartyStatsBonuses.Filter = FilterPartyStatsBonuses;
    ViewMemberStatsBonuses = new DataGridCollectionView(StatsBonuses);
    ViewMemberStatsBonuses.Filter = FilterMemberStatsBonuses;
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

  public int TotalPartyMaxTpBonus =>
    _statBonusesSection.GetTotalBonusesForTargetAndType(-1, StatBonusType.TP);

  public int TotalPartyMaxMpBonus =>
    _statBonusesSection.GetTotalBonusesForTargetAndType(-1, StatBonusType.MP);

  public int TotalMemberMaxHpBonus
  {
    get
    {
      if (SelectedMember == null)
        return 0;

      return _statBonusesSection.GetTotalBonusesForTargetAndType((int)SelectedMember.Trueid,
        StatBonusType.HP);
    }
  }

  public int TotalMemberAttackBonus
  {
    get
    {
      if (SelectedMember == null)
        return 0;

      return _statBonusesSection.GetTotalBonusesForTargetAndType((int)SelectedMember.Trueid,
        StatBonusType.Attack);
    }
  }

  public int TotalMemberDefenseBonus
  {
    get
    {
      if (SelectedMember == null)
        return 0;

      return _statBonusesSection.GetTotalBonusesForTargetAndType((int)SelectedMember.Trueid,
        StatBonusType.Defense);
    }
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
    if (SelectedMember is null)
      return;

    StatBonusInfo statBonusInfo = new();
    statBonusInfo.Target = (StatBonusTarget)SelectedMember.Trueid;
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
    OnPropertyChanged(nameof(TotalPartyMaxTpBonus));
    OnPropertyChanged(nameof(TotalPartyMaxMpBonus));
    OnPropertyChanged(nameof(TotalMemberMaxHpBonus));
    OnPropertyChanged(nameof(TotalMemberAttackBonus));
    OnPropertyChanged(nameof(TotalMemberDefenseBonus));
    ViewMemberStatsBonuses.Refresh();
    ViewPartyStatsBonuses.Refresh();
  }
}
