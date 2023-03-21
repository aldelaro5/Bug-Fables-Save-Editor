using System;
using System.Linq;
using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;
using Humanizer;
using static BugFablesLib.SaveData.StatBonusSaveData;

namespace BugFablesSaveEditor.ViewModels;

[ObservableObject]
public partial class ObservableStatsBonusSaveData : BfObservable
{
  private readonly StatBonusSaveData _statBonusSaveData;

  public string[] StatBonusTypeNames
  {
    get => Enum.GetNames(typeof(StatBonusType)).Select(x => x.Humanize(LetterCasing.Title))
      .ToArray();
  }

  [ObservableProperty]
  private ObservableBfResource _target;

  public int Type
  {
    get => (int)_statBonusSaveData.Type;
    set => SetProperty((int)_statBonusSaveData.Type, value, _statBonusSaveData,
      (x, y) => x.Type = (StatBonusType)y);
  }

  public int Amount
  {
    get => _statBonusSaveData.Amount;
    set => SetProperty(_statBonusSaveData.Amount, value, _statBonusSaveData,
      (statBonusSaveData, n) => statBonusSaveData.Amount = n);
  }

  public ObservableStatsBonusSaveData(StatBonusSaveData statBonusSaveData) :
    base(statBonusSaveData)
  {
    _statBonusSaveData = statBonusSaveData;
    _target = new ObservableBfResource(statBonusSaveData.Target);
  }
}
