using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

public class ObservableFlagSaveData : ObservableObject
{
  private readonly FlagSaveData _flagSaveData;

  public bool Enabled
  {
    get => _flagSaveData.Enabled;
    set => SetProperty(_flagSaveData.Enabled, value, _flagSaveData,
      (flagSaveData, n) => flagSaveData.Enabled = n);
  }

  public ObservableFlagSaveData(FlagSaveData flagSaveData)
  {
    _flagSaveData = flagSaveData;
  }
}
