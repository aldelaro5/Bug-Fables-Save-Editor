using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

[ObservableObject]
public partial class ObservableFlagvarSaveData : BfObservable
{
  private readonly FlagvarSaveData _flagvarSaveData;

  public int Str
  {
    get => _flagvarSaveData.Var;
    set => SetProperty(_flagvarSaveData.Var, value, _flagvarSaveData,
      (flagvarSaveData, n) => flagvarSaveData.Var = n);
  }

  public ObservableFlagvarSaveData(FlagvarSaveData flagvarSaveData) :
    base(flagvarSaveData)
  {
    _flagvarSaveData = flagvarSaveData;
  }
}
