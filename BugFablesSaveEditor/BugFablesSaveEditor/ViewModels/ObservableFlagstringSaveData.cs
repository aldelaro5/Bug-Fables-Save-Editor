using BugFablesLib.SaveData;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugFablesSaveEditor.ViewModels;

[ObservableObject]
public partial class ObservableFlagstringSaveData : BfObservable
{
  private readonly FlagstringSaveData _flagstringSaveData;

  public string Str
  {
    get => _flagstringSaveData.Str;
    set => SetProperty(_flagstringSaveData.Str, value, _flagstringSaveData,
      (flagvarSaveData, n) => flagvarSaveData.Str = n);
  }

  public ObservableFlagstringSaveData(FlagstringSaveData flagstringSaveData) :
    base(flagstringSaveData)
  {
    _flagstringSaveData = flagstringSaveData;
  }
}
