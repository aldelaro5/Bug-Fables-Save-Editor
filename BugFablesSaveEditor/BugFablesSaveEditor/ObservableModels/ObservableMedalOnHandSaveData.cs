using System.Collections.Generic;
using BugFablesLib.Data;
using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ObservableModels;

public partial class ObservableMedalOnHandSaveData : ObservableObject, IModelWrapper<BfMedalOnHandSaveData>
{
  public BfMedalOnHandSaveData Model { get; }

  public static IModelWrapper<BfMedalOnHandSaveData> WrapModel(BfMedalOnHandSaveData model) =>
    new ObservableMedalOnHandSaveData(model);

  [ObservableProperty]
  private ObservableBfNamedId _medal;

  public int MedalEquipTarget
  {
    get => Model.MedalEquipTarget + 2;
    set => SetProperty(Model.MedalEquipTarget, value - 2, Model,
      (data, i) => data.MedalEquipTarget = i);
  }

  public IReadOnlyList<string> MedalEquipTargets { get; }

  public ObservableMedalOnHandSaveData(BfMedalOnHandSaveData medalOnHandSaveData)
  {
    Model = medalOnHandSaveData;
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
