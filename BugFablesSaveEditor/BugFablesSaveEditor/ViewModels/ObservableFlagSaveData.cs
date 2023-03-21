using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

[ObservableObject]
public partial class ObservableFlagSaveData : BfObservable
{
  private readonly FlagSaveData _flagSaveData;

  public bool Enabled
  {
    get => _flagSaveData.Enabled;
    set => SetProperty(_flagSaveData.Enabled, value, _flagSaveData,
      (flagSaveData, n) => flagSaveData.Enabled = n);
  }

  public ObservableFlagSaveData(FlagSaveData flagSaveData) :
    base(flagSaveData)
  {
    _flagSaveData = flagSaveData;
  }
}
