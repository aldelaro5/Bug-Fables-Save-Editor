using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ObservableModels;

public class ObservableFlagvarSaveData : ObservableObject, IModelWrapper<FlagvarSaveData>
{
  public FlagvarSaveData Model { get; }

  public int Var
  {
    get => Model.Var;
    set => SetProperty(Model.Var, value, Model, (data, s) => data.Var = s);
  }

  public ObservableFlagvarSaveData(FlagvarSaveData flagvarSaveData) => Model = flagvarSaveData;
}
