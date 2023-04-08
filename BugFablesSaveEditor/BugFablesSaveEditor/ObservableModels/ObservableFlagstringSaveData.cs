using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ObservableModels;

public class ObservableFlagstringSaveData : ObservableObject
{
  private readonly FlagstringSaveData _model;

  public string Str
  {
    get => _model.Str;
    set => SetProperty(_model.Str, value, _model, (data, s) => data.Str = s);
  }

  public ObservableFlagstringSaveData(FlagstringSaveData flagstringSaveData) => _model = flagstringSaveData;
}
