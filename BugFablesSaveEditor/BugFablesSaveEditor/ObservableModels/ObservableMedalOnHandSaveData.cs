using System.Collections.Generic;
using BugFablesLib.Data;
using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ObservableModels;

public partial class ObservableMedalOnHandSaveData : ObservableModel
{
  public sealed override BfMedalOnHandSaveData UnderlyingData { get; }

  [ObservableProperty]
  private ObservableBfNamedId _medal;

  public int MedalEquipTarget
  {
    get => UnderlyingData.MedalEquipTarget + 2;
    set => SetProperty(UnderlyingData.MedalEquipTarget, value - 2, UnderlyingData,
      (data, i) => data.MedalEquipTarget = i);
  }

  public IReadOnlyList<string> MedalEquipTargets { get; }

  public ObservableMedalOnHandSaveData(BfMedalOnHandSaveData medalOnHandSaveData) :
    base(medalOnHandSaveData)
  {
    UnderlyingData = medalOnHandSaveData;
    _medal = new ObservableBfNamedId(medalOnHandSaveData.Medal);
    MedalEquipTargets = GenerateMedalEquipTargetsList();
  }

  private IReadOnlyList<string> GenerateMedalEquipTargetsList()
  {
    var list = new List<string> { "Unequipped", "Party" };
    list.AddRange(BugFablesLib.Utils.GetAllBfNames(new BfAnimId()));
    return list;
  }
}
