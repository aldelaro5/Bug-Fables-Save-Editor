using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

public class ObaservableFlagvarSaveData : ObservableObject
{
  private readonly FlagvarSaveData _flagvarSaveData;

  public int Str
  {
    get => _flagvarSaveData.Var;
    set => SetProperty(_flagvarSaveData.Var, value, _flagvarSaveData,
      (flagvarSaveData, n) => flagvarSaveData.Var = n);
  }

  public ObaservableFlagvarSaveData(FlagvarSaveData flagvarSaveData)
  {
    _flagvarSaveData = flagvarSaveData;
  }
}
