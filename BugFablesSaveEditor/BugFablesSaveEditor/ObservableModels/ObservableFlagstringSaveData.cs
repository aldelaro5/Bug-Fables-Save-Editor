using BugFablesLib.SaveData;
using Reactive.Bindings;

namespace BugFablesSaveEditor.ObservableModels;

public class ObservableFlagstringSaveData : ObservableModel
{
  public sealed override FlagstringSaveData UnderlyingData { get; }
  public ReactiveProperty<string> Str { get; }

  public ObservableFlagstringSaveData(FlagstringSaveData flagstringSaveData) : base(
    flagstringSaveData)
  {
    UnderlyingData = flagstringSaveData;
    Str = ReactiveProperty.FromObject(UnderlyingData, data => data.Str);
  }
}
