using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using BugFablesLib.SaveData;
using BugFablesSaveEditor.ObservableModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using DynamicData.Binding;

namespace BugFablesSaveEditor.ViewModels;

public partial class StatsViewModel : ObservableObject
{
  private readonly GlobalSaveData _globalSaveData;
  private readonly ViewModelCollection<StatBonusSaveData, ObservableStatsBonusSaveData> _statsBonuses;

  [ObservableProperty]
  private ReadOnlyObservableCollection<ObservableStatsBonusSaveData> _partyStatBonuses;

  [ObservableProperty]
  private ReadOnlyObservableCollection<ObservableStatsBonusSaveData> _memberStatBonuses;

  [ObservableProperty]
  private ViewModelCollection<PartyMemberSaveData, ObservablePartyMemberSaveData> _partyMembers;

  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(TotalMemberMaxHpBonus))]
  [NotifyPropertyChangedFor(nameof(TotalMemberAttackBonus))]
  [NotifyPropertyChangedFor(nameof(TotalMemberDefenseBonus))]
  [NotifyCanExecuteChangedFor(nameof(AddStatPartyMemberBonusCommand))]
  private ObservablePartyMemberSaveData? _selectedPartyMember;

  public int Mp
  {
    get => _globalSaveData.Mp;
    set => SetProperty(_globalSaveData.Mp, value, _globalSaveData, (data, s) => data.Mp = s);
  }

  public int MaxMp
  {
    get => _globalSaveData.MaxMp;
    set => SetProperty(_globalSaveData.MaxMp, value, _globalSaveData, (data, s) => data.MaxMp = s);
  }

  public int Tp
  {
    get => _globalSaveData.Tp;
    set => SetProperty(_globalSaveData.Tp, value, _globalSaveData, (data, s) => data.Tp = s);
  }

  public int MaxTp
  {
    get => _globalSaveData.MaxTp;
    set => SetProperty(_globalSaveData.MaxTp, value, _globalSaveData, (data, s) => data.MaxTp = s);
  }


  [ObservableProperty]
  private ObservableStatsBonusSaveData _newPartyStatBonus = new(new StatBonusSaveData());

  [ObservableProperty]
  private ObservableStatsBonusSaveData _newMemberStatBonus = new(new StatBonusSaveData());

  private IReadOnlyList<string> StatBonusTypeNames => Enum.GetNames<StatBonusSaveData.StatBonusType>();

  public IReadOnlyList<string> MemberStatBonusTypeNames => StatBonusTypeNames.Take(3).ToList();

  public IReadOnlyList<string> PartyStatBonusTypeNames => StatBonusTypeNames.Skip(3).ToList();

  public int TotalPartyMaxTpBonus => _statsBonuses.Collection
    .Where(x => x.Target == -1 && x.Type == StatBonusSaveData.StatBonusType.TP)
    .Sum(x => x.Amount);

  public int TotalPartyMaxMpBonus => _statsBonuses.Collection
    .Where(x => x.Target == -1 && x.Type == StatBonusSaveData.StatBonusType.MP)
    .Sum(x => x.Amount);

  public int TotalMemberMaxHpBonus => _statsBonuses.Collection
    .Where(x => x.Target == SelectedPartyMember?.AnimId.Id &&
                x.Type == StatBonusSaveData.StatBonusType.HP)
    .Sum(x => x.Amount);

  public int TotalMemberAttackBonus => _statsBonuses.Collection
    .Where(x => x.Target == SelectedPartyMember?.AnimId.Id &&
                x.Type == StatBonusSaveData.StatBonusType.Attack)
    .Sum(x => x.Amount);

  public int TotalMemberDefenseBonus => _statsBonuses.Collection
    .Where(x => x.Target == SelectedPartyMember?.AnimId.Id &&
                x.Type == StatBonusSaveData.StatBonusType.Defense)
    .Sum(x => x.Amount);

  [RelayCommand]
  private void AddStatPartyBonus(ObservableStatsBonusSaveData statsBonusSaveData)
  {
    statsBonusSaveData.Target = -1;
    _statsBonuses.AddViewModelCommand.Execute(statsBonusSaveData);
    OnPropertyChanged(nameof(TotalPartyMaxMpBonus));
    OnPropertyChanged(nameof(TotalPartyMaxTpBonus));
  }

  [RelayCommand(CanExecute = nameof(CanAddPartyMemberBonus))]
  private void AddStatPartyMemberBonus(ObservableStatsBonusSaveData statsBonusSaveData)
  {
    statsBonusSaveData.Target = SelectedPartyMember!.AnimId.Id;
    _statsBonuses.AddViewModelCommand.Execute(statsBonusSaveData);
    OnPropertyChanged(nameof(TotalMemberMaxHpBonus));
    OnPropertyChanged(nameof(TotalMemberAttackBonus));
    OnPropertyChanged(nameof(TotalMemberDefenseBonus));
  }

  private bool CanAddPartyMemberBonus() => SelectedPartyMember is not null;

  [RelayCommand]
  private void DeleteStatBonus(ObservableStatsBonusSaveData statsBonus) =>
    _statsBonuses.RemoveViewModelCommand.Execute(statsBonus);

  public StatsViewModel() : this(new(), new(), new()) { }

  public StatsViewModel(Collection<StatBonusSaveData> statsBonuses,
                        Collection<PartyMemberSaveData> partyMembers,
                        GlobalSaveData globalSaveData)
  {
    _statsBonuses = new(statsBonuses);
    _partyMembers = new(partyMembers);
    _globalSaveData = globalSaveData;

    _statsBonuses.Collection.ToObservableChangeSet()
      .Filter(x => x.Target == -1)
      .ObserveOn(SynchronizationContext.Current!)
      .Bind(out _partyStatBonuses)
      .Subscribe();

    var memberFilter = this.WhenValueChanged(x => x.SelectedPartyMember)
      .Select(member =>
      {
        return new Func<ObservableStatsBonusSaveData, bool>(statsBonusSaveData =>
          member?.AnimId.Id == statsBonusSaveData.Target);
      });

    _statsBonuses.Collection
      .ToObservableChangeSet()
      .Filter(memberFilter)
      .ObserveOn(SynchronizationContext.Current!)
      .Bind(out _memberStatBonuses)
      .Subscribe(_ => OnPropertyChanged(nameof(MemberStatBonuses)));
  }
}
