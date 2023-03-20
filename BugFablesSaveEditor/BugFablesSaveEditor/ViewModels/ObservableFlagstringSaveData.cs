using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

public class ObservableFlagstringSaveData : ObservableObject
{
  private readonly FlagstringSaveData _flagstringSaveData;

  public string Str
  {
    get => _flagstringSaveData.Str;
    set => SetProperty(_flagstringSaveData.Str, value, _flagstringSaveData,
      (flagvarSaveData, n) => flagvarSaveData.Str = n);
  }

  public ObservableFlagstringSaveData(FlagstringSaveData flagvarSaveData)
  {
    _flagstringSaveData = flagvarSaveData;
  }
}
