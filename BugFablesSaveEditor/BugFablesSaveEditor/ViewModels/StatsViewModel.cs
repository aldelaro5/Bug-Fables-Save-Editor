using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BugFablesLib.SaveData;
using BugFablesSaveEditor.ObservableModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using DynamicData.Binding;
using Reactive.Bindings.Extensions;
using Reactive.Bindings.TinyLinq;

namespace BugFablesSaveEditor.ViewModels;

public partial class StatsViewModel : ObservableObject
{
  private readonly ObservableBfCollection<StatBonusSaveData, ObservableStatsBonusSaveData>
    _statsBonuses;

  [ObservableProperty]
  private ReadOnlyObservableCollection<ObservableStatsBonusSaveData> _partyStatBonuses = null!;

  [ObservableProperty]
  private ReadOnlyObservableCollection<ObservableStatsBonusSaveData> _memberStatBonuses = null!;

  [ObservableProperty]
  private ObservableBfCollection<PartyMemberSaveData, ObservablePartyMemberSaveData> _partyMembers;

  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(TotalMemberMaxHpBonus))]
  [NotifyPropertyChangedFor(nameof(TotalMemberAttackBonus))]
  [NotifyPropertyChangedFor(nameof(TotalMemberDefenseBonus))]
  [NotifyCanExecuteChangedFor(nameof(AddStatPartyMemberBonusCommand))]
  private ObservablePartyMemberSaveData? _selectedPartyMember;

  [ObservableProperty]
  private ObservableGlobalSaveData _globalSaveData;

  [ObservableProperty]
  private ObservableStatsBonusSaveData _newPartyStatBonus = new(new StatBonusSaveData());

  [ObservableProperty]
  private ObservableStatsBonusSaveData _newMemberStatBonus = new(new StatBonusSaveData());

  private IReadOnlyList<string> StatBonusTypeNames =>
    Enum.GetNames<StatBonusSaveData.StatBonusType>();

  public IReadOnlyList<string> MemberStatBonusTypeNames =>
    StatBonusTypeNames.Take(3).ToList();

  public IReadOnlyList<string> PartyStatBonusTypeNames =>
    StatBonusTypeNames.Skip(3).ToList();

  public int TotalPartyMaxTpBonus => _statsBonuses
    .Where(x => x.Target == -1 && x.Type == StatBonusSaveData.StatBonusType.TP)
    .Sum(x => x.Amount);

  public int TotalPartyMaxMpBonus => _statsBonuses
    .Where(x => x.Target == -1 && x.Type == StatBonusSaveData.StatBonusType.MP)
    .Sum(x => x.Amount);

  public int TotalMemberMaxHpBonus => _statsBonuses
    .Where(x => x.Target == SelectedPartyMember?.AnimId.Id &&
                x.Type == StatBonusSaveData.StatBonusType.HP)
    .Sum(x => x.Amount);

  public int TotalMemberAttackBonus => _statsBonuses
    .Where(x => x.Target == SelectedPartyMember?.AnimId.Id &&
                x.Type == StatBonusSaveData.StatBonusType.Attack)
    .Sum(x => x.Amount);

  public int TotalMemberDefenseBonus => _statsBonuses
    .Where(x => x.Target == SelectedPartyMember?.AnimId.Id &&
                x.Type == StatBonusSaveData.StatBonusType.Defense)
    .Sum(x => x.Amount);

  [RelayCommand]
  private void AddStatPartyBonus(ObservableStatsBonusSaveData statsBonusSaveData)
  {
    statsBonusSaveData.Target = -1;
    _statsBonuses.Add(new(statsBonusSaveData.UnderlyingData));
    OnPropertyChanged(nameof(TotalPartyMaxMpBonus));
    OnPropertyChanged(nameof(TotalPartyMaxTpBonus));
  }

  [RelayCommand(CanExecute = nameof(CanAddPartyMemberBonus))]
  private void AddStatPartyMemberBonus(ObservableStatsBonusSaveData statsBonusSaveData)
  {
    statsBonusSaveData.Target = SelectedPartyMember!.AnimId.Id;
    _statsBonuses.Add(new(statsBonusSaveData.UnderlyingData));
    OnPropertyChanged(nameof(TotalMemberMaxHpBonus));
    OnPropertyChanged(nameof(TotalMemberAttackBonus));
    OnPropertyChanged(nameof(TotalMemberDefenseBonus));
  }

  private bool CanAddPartyMemberBonus() => SelectedPartyMember is not null;

  [RelayCommand]
  private void DeleteStatBonus(ObservableStatsBonusSaveData statsBonus) =>
    _statsBonuses.Remove(statsBonus);

  public StatsViewModel(
    ObservableBfCollection<StatBonusSaveData, ObservableStatsBonusSaveData> statsBonuses,
    ObservableBfCollection<PartyMemberSaveData, ObservablePartyMemberSaveData> partyMembers,
    ObservableGlobalSaveData globalSaveData)
  {
    _statsBonuses = statsBonuses;
    _partyMembers = partyMembers;
    _globalSaveData = globalSaveData;

    _statsBonuses.ToObservableChangeSet()
      .Filter(x => x.Target == -1)
      .ObserveOnUIDispatcher()
      .Bind(out _partyStatBonuses)
      .Subscribe();

    var memberFilter = this.WhenValueChanged(x => x.SelectedPartyMember)
      .Select(member =>
      {
        return new Func<ObservableStatsBonusSaveData, bool>(statsBonusSaveData =>
          member?.AnimId.Id == statsBonusSaveData.Target);
      });

    _statsBonuses.ToObservableChangeSet()
      .Filter(memberFilter)
      .ObserveOnUIDispatcher()
      .Bind(out _memberStatBonuses)
      .Subscribe(_ => OnPropertyChanged(nameof(MemberStatBonuses)));
  }

  public StatsViewModel()
  {
    _statsBonuses = new(new(), _ => new List<ObservableStatsBonusSaveData>());
    _partyMembers = new(new(), _ => new List<ObservablePartyMemberSaveData>());
    _globalSaveData = new(new());
  }
}
