using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using BugFablesLib.SaveData;
using BugFablesSaveEditor.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using DynamicData.Binding;
using static BugFablesLib.SaveData.StatBonusSaveData;

namespace BugFablesSaveEditor.Core.ViewModels;

public partial class StatsViewModel : ObservableObject, IDisposable
{
  private readonly IDisposable _partyMemberStatsBonusesDisposable;
  private readonly IDisposable _partyStatsBonusesDisposable;
  private readonly GlobalSaveData _globalSaveData;
  private readonly ViewModelCollection<StatBonusSaveData, StatsBonusSaveDataModel> _statsBonuses;

  [ObservableProperty]
  private ReadOnlyObservableCollection<StatsBonusSaveDataModel> _partyStatBonuses;

  [ObservableProperty]
  private ReadOnlyObservableCollection<StatsBonusSaveDataModel> _memberStatBonuses;

  [ObservableProperty]
  private ViewModelCollection<PartyMemberSaveData, PartyMemberSaveDataModel> _partyMembers;

  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(TotalMemberMaxHpBonus))]
  [NotifyPropertyChangedFor(nameof(TotalMemberAttackBonus))]
  [NotifyPropertyChangedFor(nameof(TotalMemberDefenseBonus))]
  [NotifyCanExecuteChangedFor(nameof(AddStatPartyMemberBonusCommand))]
  private PartyMemberSaveDataModel? _selectedPartyMember;

  [ObservableProperty]
  private StatsBonusSaveDataModel _newPartyStatBonus = new(new StatBonusSaveData { Amount = 1 });

  [ObservableProperty]
  private StatsBonusSaveDataModel _newMemberStatBonus = new(new StatBonusSaveData { Amount = 1 });

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

  public IReadOnlyList<string> StatBonusTypeNames => Enum.GetNames<StatBonusType>();

  private int GetSumForAnimIdAndBonusType(int? animId, StatBonusType type)
  {
    return _statsBonuses.Collection
      .Where(x => x.Target == animId && x.Type == type)
      .Sum(x => x.Amount);
  }

  public int TotalPartyMaxHpBonus => GetSumForAnimIdAndBonusType(-1, StatBonusType.HP);
  public int TotalPartyAttackBonus => GetSumForAnimIdAndBonusType(-1, StatBonusType.Attack);
  public int TotalPartyDefenseBonus => GetSumForAnimIdAndBonusType(-1, StatBonusType.Defense);
  public int TotalPartyMaxTpBonus => GetSumForAnimIdAndBonusType(-1, StatBonusType.TP);
  public int TotalPartyMaxMpBonus => GetSumForAnimIdAndBonusType(-1, StatBonusType.MP);


  public int TotalMemberMaxHpBonus => GetSumForAnimIdAndBonusType(SelectedPartyMember?.AnimId.Id, StatBonusType.HP);

  public int TotalMemberAttackBonus =>
    GetSumForAnimIdAndBonusType(SelectedPartyMember?.AnimId.Id, StatBonusType.Attack);

  public int TotalMemberDefenseBonus =>
    GetSumForAnimIdAndBonusType(SelectedPartyMember?.AnimId.Id, StatBonusType.Defense);

  public int TotalMemberTpBonus => GetSumForAnimIdAndBonusType(SelectedPartyMember?.AnimId.Id, StatBonusType.TP);
  public int TotalMemberMpBonus => GetSumForAnimIdAndBonusType(SelectedPartyMember?.AnimId.Id, StatBonusType.MP);

  public StatsViewModel() : this(new(), new(new()), new()) { }

  public StatsViewModel(Collection<StatBonusSaveData> statsBonuses,
                        ViewModelCollection<PartyMemberSaveData, PartyMemberSaveDataModel> partyMembers,
                        GlobalSaveData globalSaveData)
  {
    _statsBonuses = new(statsBonuses);
    _partyMembers = partyMembers;
    _globalSaveData = globalSaveData;

    _partyStatsBonusesDisposable = SetupFilteredPartyBonuses()
      .Bind(out _partyStatBonuses)
      .Subscribe();
    _partyMemberStatsBonusesDisposable = SetupFilteredMemberBonuses()
      .Bind(out _memberStatBonuses)
      .Subscribe(_ => OnPropertyChanged(nameof(MemberStatBonuses)));
  }

  private IObservable<IChangeSet<StatsBonusSaveDataModel>> SetupFilteredMemberBonuses()
  {
    var memberFilter = this.WhenValueChanged<StatsViewModel, PartyMemberSaveDataModel>(x => x.SelectedPartyMember!)
      .Select(SelectedPartyMemberFilter);

    var filteredMemberBonuses = _statsBonuses.Collection
      .ToObservableChangeSet()
      .Filter(memberFilter);

    return FilterUtils.ObserveOnSafeThread(filteredMemberBonuses);
  }

  private IObservable<IChangeSet<StatsBonusSaveDataModel>> SetupFilteredPartyBonuses()
  {
    var filteredPartyBonuses = _statsBonuses.Collection
      .ToObservableChangeSet()
      .Filter(x => x.Target == -1);

    return FilterUtils.ObserveOnSafeThread(filteredPartyBonuses);
  }

  [RelayCommand]
  private void AddStatPartyBonus(StatsBonusSaveDataModel statsBonusSaveData)
  {
    statsBonusSaveData.Target = -1;
    _statsBonuses.AddViewModelCommand.Execute(statsBonusSaveData);
    RefreshPartySums();
  }

  private void RefreshPartySums()
  {
    OnPropertyChanged(nameof(TotalPartyMaxHpBonus));
    OnPropertyChanged(nameof(TotalPartyAttackBonus));
    OnPropertyChanged(nameof(TotalPartyDefenseBonus));
    OnPropertyChanged(nameof(TotalPartyMaxMpBonus));
    OnPropertyChanged(nameof(TotalPartyMaxTpBonus));
  }

  [RelayCommand(CanExecute = nameof(CanAddPartyMemberBonus))]
  private void AddStatPartyMemberBonus(StatsBonusSaveDataModel statsBonusSaveData)
  {
    statsBonusSaveData.Target = SelectedPartyMember!.AnimId.Id;
    _statsBonuses.AddViewModelCommand.Execute(statsBonusSaveData);
    RefreshMemberSums();
  }

  private void RefreshMemberSums()
  {
    OnPropertyChanged(nameof(TotalMemberMaxHpBonus));
    OnPropertyChanged(nameof(TotalMemberAttackBonus));
    OnPropertyChanged(nameof(TotalMemberDefenseBonus));
    OnPropertyChanged(nameof(TotalMemberTpBonus));
    OnPropertyChanged(nameof(TotalMemberMpBonus));
  }

  private bool CanAddPartyMemberBonus() => SelectedPartyMember is not null;

  [RelayCommand]
  private void DeleteStatBonus(StatsBonusSaveDataModel statsBonus)
  {
    _statsBonuses.RemoveViewModelCommand.Execute(statsBonus);
    RefreshPartySums();
    RefreshMemberSums();
  }

  private static Func<StatsBonusSaveDataModel, bool> SelectedPartyMemberFilter(PartyMemberSaveDataModel? member)
  {
    if (member is null)
      return _ => false;

    return statsBonusSaveData => member.AnimId.Id == statsBonusSaveData.Target;
  }

  public void Dispose()
  {
    _partyMemberStatsBonusesDisposable.Dispose();
    _partyStatsBonusesDisposable.Dispose();
    _statsBonuses.Dispose();
  }
}
