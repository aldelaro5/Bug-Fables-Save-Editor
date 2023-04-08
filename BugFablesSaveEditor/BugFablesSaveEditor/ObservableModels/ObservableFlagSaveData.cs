using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ObservableModels;

public class ObservableFlagSaveData : ObservableObject
{
  private readonly FlagSaveData _model;

  public bool Enabled
  {
    get => _model.Enabled;
    set => SetProperty(_model.Enabled, value, _model, (data, b) => data.Enabled = b);
  }

  public ObservableFlagSaveData(FlagSaveData flagSaveData) => _model = flagSaveData;
}
