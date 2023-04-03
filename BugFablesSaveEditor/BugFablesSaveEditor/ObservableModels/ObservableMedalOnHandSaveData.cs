using System.Collections.Generic;
using BugFablesLib.Data;
using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;
using Reactive.Bindings;

namespace BugFablesSaveEditor.ObservableModels;

public partial class ObservableMedalOnHandSaveData : ObservableModel
{
  public sealed override BfMedalOnHandSaveData UnderlyingData { get; }

  [ObservableProperty]
  private ObservableBfNamedId _medal;

  public ReactiveProperty<int> MedalEquipTarget { get; }

  public IReadOnlyList<string> MedalEquipTargets { get; }

  public ObservableMedalOnHandSaveData(BfMedalOnHandSaveData medalOnHandSaveData) :
    base(medalOnHandSaveData)
  {
    UnderlyingData = medalOnHandSaveData;
    _medal = new ObservableBfNamedId(medalOnHandSaveData.Medal);
    MedalEquipTargets = GenerateMedalEquipTargetsList();
    MedalEquipTarget = ReactiveProperty.FromObject(UnderlyingData, data => data.MedalEquipTarget,
      convert: i => i + 2,
      convertBack: i => i - 2);
  }

  private IReadOnlyList<string> GenerateMedalEquipTargetsList()
  {
    var list = new List<string> { "Unequipped", "Party" };
    list.AddRange(BugFablesLib.Utils.GetAllBfNames(new BfAnimId()));
    return list;
  }
}
