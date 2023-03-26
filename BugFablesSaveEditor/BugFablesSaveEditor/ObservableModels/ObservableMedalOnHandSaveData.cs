using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;
using Reactive.Bindings;

namespace BugFablesSaveEditor.ObservableModels;

public partial class ObservableMedalOnHandSaveData : ObservableModel
{
  public sealed override MedalOnHandSaveData UnderlyingData { get; }

  [ObservableProperty]
  private ObservableBfNamedId _medal;

  public ReactiveProperty<int> MedalEquipTarget { get; }

  public ObservableMedalOnHandSaveData(MedalOnHandSaveData medalOnHandSaveData) :
    base(medalOnHandSaveData)
  {
    UnderlyingData = medalOnHandSaveData;
    _medal = new ObservableBfNamedId(medalOnHandSaveData.Medal);
    MedalEquipTarget = ReactiveProperty.FromObject(UnderlyingData, data => data.MedalEquipTarget);
  }
}
