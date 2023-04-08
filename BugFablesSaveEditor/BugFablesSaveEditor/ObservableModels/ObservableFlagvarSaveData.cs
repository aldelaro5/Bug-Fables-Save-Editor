using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ObservableModels;

public class ObservableFlagvarSaveData : ObservableObject
{
  private readonly FlagvarSaveData _model;

  public int Var
  {
    get => _model.Var;
    set => SetProperty(_model.Var, value, _model, (data, s) => data.Var = s);
  }

  public ObservableFlagvarSaveData(FlagvarSaveData flagvarSaveData) => _model = flagvarSaveData;
}
