using BugFablesLib.SaveData;

namespace BugFablesSaveEditor.ObservableModels;

public class ObservableFlagstringSaveData : ObservableModel
{
  public sealed override FlagstringSaveData UnderlyingData { get; }

  public string Str
  {
    get => UnderlyingData.Str;
    set => SetProperty(UnderlyingData.Str, value, UnderlyingData, (data, s) => data.Str = s);
  }

  public ObservableFlagstringSaveData(FlagstringSaveData flagstringSaveData) : base(
    flagstringSaveData)
  {
    UnderlyingData = flagstringSaveData;
  }
}
