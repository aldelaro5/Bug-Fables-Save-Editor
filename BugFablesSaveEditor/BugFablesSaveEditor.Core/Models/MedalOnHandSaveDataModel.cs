using System.Collections.Generic;
using BugFablesLib.Data;
using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.Core.Models;

public partial class MedalOnHandSaveDataModel : ObservableObject, IModelWrapper<BfMedalOnHandSaveData>
{
  public BfMedalOnHandSaveData Model { get; }

  [ObservableProperty]
  private BfNamedIdModel _medal;

  public int MedalEquipTarget
  {
    get => Model.MedalEquipTarget + 2;
    set => SetProperty(Model.MedalEquipTarget, value - 2, Model,
      (data, i) => data.MedalEquipTarget = i);
  }

  public IReadOnlyList<string> MedalEquipTargets { get; }

  public static IModelWrapper<BfMedalOnHandSaveData> WrapModel(BfMedalOnHandSaveData model) =>
    new MedalOnHandSaveDataModel(model);

  public static IModelWrapper<BfMedalOnHandSaveData> WrapNewModel(BfMedalOnHandSaveData model) =>
    new MedalOnHandSaveDataModel(new BfMedalOnHandSaveData
      { Medal = model.Medal, MedalEquipTarget = model.MedalEquipTarget });

  private MedalOnHandSaveDataModel(BfMedalOnHandSaveData medalOnHandSaveData)
  {
    Model = medalOnHandSaveData;
    _medal = new BfNamedIdModel(medalOnHandSaveData.Medal);
    MedalEquipTargets = GenerateMedalEquipTargetsList();
  }

  private IReadOnlyList<string> GenerateMedalEquipTargetsList()
  {
    var list = new List<string> { "Unequipped", "Party" };
    list.AddRange(BugFablesLib.Utils.GetAllBfNames(new BfAnimId()));
    list[2] = "Vi";
    list[3] = "Kabbu";
    list[4] = "Leif";
    return list;
  }
}
