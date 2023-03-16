using System.Collections.Generic;
using Avalonia.Collections;
using BugFablesDataLib;
using BugFablesDataLib.Sections;
using BugFablesSaveEditor.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using static BugFablesDataLib.Sections.PartyMembers;
using static BugFablesDataLib.Sections.StatBonuses;

namespace BugFablesSaveEditor.ViewModels;

public partial class StatsViewModel : ObservableObject
{
  private readonly StatBonuses _statBonusesSection;

  [ObservableProperty]
  private SaveData _saveData;

  [ObservableProperty]
  private IList<PartyMemberInfo> _partyMembers;

  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(TotalPartyMaxTpBonus))]
  [NotifyPropertyChangedFor(nameof(TotalPartyMaxMpBonus))]
  [NotifyPropertyChangedFor(nameof(TotalMemberMaxHpBonus))]
  [NotifyPropertyChangedFor(nameof(TotalMemberAttackBonus))]
  [NotifyPropertyChangedFor(nameof(TotalMemberDefenseBonus))]
  [NotifyCanExecuteChangedFor(nameof(AddMemberStatBonusCommand))]
  private PartyMemberInfo? _selectedMember;

  partial void OnSelectedMemberChanged(PartyMemberInfo? value) => RefreshViews();

  [ObservableProperty]
  private StatBonusType _statBonusTypeMemberSelectedForAdd;

  [ObservableProperty]
  private StatBonusType _statBonusTypePartySelectedForAdd;

  [ObservableProperty]
  private string[] _statBonusTypes;

  [ObservableProperty]
  private int _statsBonusAmountMemberSelectedForAdd;

  [ObservableProperty]
  private int _statsBonusAmountPartySelectedForAdd;

  [ObservableProperty]
  private IList<StatBonusInfo> _statsBonuses;

  [ObservableProperty]
  private DataGridCollectionView _viewMemberStatsBonuses;

  [ObservableProperty]
  private DataGridCollectionView _viewPartyStatsBonuses;

  public StatsViewModel() : this(new SaveData())
  {
    PartyMembers.Add(new PartyMemberInfo { AnimId = AnimID.Bee });
    PartyMembers.Add(new PartyMemberInfo { AnimId = AnimID.Beetle });
    PartyMembers.Add(new PartyMemberInfo { AnimId = AnimID.Moth });

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
    _saveData = saveData;
    _statBonusTypes = Utils.GetEnumDescriptions<StatBonusType>();
    _statBonusesSection = _saveData.StatBonuses;
    _partyMembers = _saveData.PartyMembers.List;
    _statsBonuses = _saveData.StatBonuses.List;
    _viewPartyStatsBonuses = new(StatsBonuses);
    _viewPartyStatsBonuses.Filter = arg =>
    {
      StatBonusInfo statBonusInfo = (StatBonusInfo)arg;
      return statBonusInfo.Target == StatBonusTarget.Party;
    };
    _viewMemberStatsBonuses = new(StatsBonuses);
    _viewMemberStatsBonuses.Filter = arg =>
    {
      StatBonusInfo statBonusInfo = (StatBonusInfo)arg;
      if (SelectedMember == null)
        return false;

      return (int)statBonusInfo.Target == (int)SelectedMember.AnimId;
    };
  }

  [RelayCommand(CanExecute = nameof(CanRemovePartyStatBonus))]
  private void CmdRemovePartyStatBonus(StatBonusInfo info)
  {
    ViewPartyStatsBonuses.Remove(info);
    RefreshViews();
  }

  private bool CanRemovePartyStatBonus(StatBonusInfo info) => !ViewPartyStatsBonuses.IsEditingItem;

  [RelayCommand(CanExecute = nameof(CanRemoveMemberStatBonus))]
  private void CmdRemoveMemberStatBonus(StatBonusInfo info)
  {
    ViewMemberStatsBonuses.Remove(info);
    RefreshViews();
  }

  private bool CanRemoveMemberStatBonus(StatBonusInfo info) =>
    !ViewMemberStatsBonuses.IsEditingItem;

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

      return _statBonusesSection.GetTotalBonusesForTargetAndType((int)SelectedMember.AnimId,
        StatBonusType.HP);
    }
  }

  public int TotalMemberAttackBonus
  {
    get
    {
      if (SelectedMember == null)
        return 0;

      return _statBonusesSection.GetTotalBonusesForTargetAndType((int)SelectedMember.AnimId,
        StatBonusType.Attack);
    }
  }

  public int TotalMemberDefenseBonus
  {
    get
    {
      if (SelectedMember == null)
        return 0;

      return _statBonusesSection.GetTotalBonusesForTargetAndType((int)SelectedMember.AnimId,
        StatBonusType.Defense);
    }
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
    statBonusInfo.Target = (StatBonusTarget)SelectedMember.AnimId;
    statBonusInfo.Type = StatBonusTypeMemberSelectedForAdd;
    statBonusInfo.Amount = StatsBonusAmountMemberSelectedForAdd;
    StatsBonuses.Add(statBonusInfo);
    RefreshViews();
  }

  private bool CanAddMemberStatBonus() => SelectedMember is not null;

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
