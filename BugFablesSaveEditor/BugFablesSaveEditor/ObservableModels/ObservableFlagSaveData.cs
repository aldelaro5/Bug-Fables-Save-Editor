using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ObservableModels;

public class ObservableFlagSaveData : ObservableObject, IModelWrapper<FlagSaveData>
{
  public FlagSaveData Model { get; }
  public static IModelWrapper<FlagSaveData> WrapModel(FlagSaveData model) => new ObservableFlagSaveData(model);

  public bool Enabled
  {
    get => Model.Enabled;
    set => SetProperty(Model.Enabled, value, Model, (data, b) => data.Enabled = b);
  }

  public ObservableFlagSaveData(FlagSaveData flagSaveData) => Model = flagSaveData;
}
