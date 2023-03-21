using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

[ObservableObject]
public partial class ObservableMedalOnHandSaveData : BfObservable
{
  private readonly MedalOnHandSaveData _medalOnHandSaveData;

  [ObservableProperty]
  private ObservableBfResource _medal;

  public int MedalEquipTarget
  {
    get => _medalOnHandSaveData.MedalEquipTarget;
    set => SetProperty(_medalOnHandSaveData.MedalEquipTarget, value, _medalOnHandSaveData,
      (medalOnHandSaveData, n) => medalOnHandSaveData.MedalEquipTarget = n);
  }

  public ObservableMedalOnHandSaveData(MedalOnHandSaveData medalOnHandSaveData) :
    base(medalOnHandSaveData)
  {
    _medalOnHandSaveData = medalOnHandSaveData;
    _medal = new ObservableBfResource(medalOnHandSaveData.Medal);
  }
}
