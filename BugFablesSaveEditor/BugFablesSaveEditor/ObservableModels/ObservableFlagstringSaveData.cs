using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ObservableModels;

public class ObservableFlagstringSaveData : ObservableObject, IModelWrapper<FlagstringSaveData>
{
  public FlagstringSaveData Model { get; }

  public static IModelWrapper<FlagstringSaveData> Factory(FlagstringSaveData model) =>
    new ObservableFlagstringSaveData(model);

  public string Str
  {
    get => Model.Str;
    set => SetProperty(Model.Str, value, Model, (data, s) => data.Str = s);
  }

  public ObservableFlagstringSaveData(FlagstringSaveData flagstringSaveData) => Model = flagstringSaveData;
}
