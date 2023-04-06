using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ObservableModels;

public class ObservableFlagSaveData : ObservableObject, IModelWrapper
{
  object IModelWrapper.Model { get => Model; }
  public FlagSaveData Model { get; }

  public bool Enabled
  {
    get => Model.Enabled;
    set => SetProperty(Model.Enabled, value, Model, (data, b) => data.Enabled = b);
  }

  public ObservableFlagSaveData(FlagSaveData flagSaveData) => Model = flagSaveData;
}
