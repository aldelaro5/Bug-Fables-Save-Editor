using BugFablesLib.SaveData;
using Reactive.Bindings;

namespace BugFablesSaveEditor.ObservableModels;

public class ObservableFlagvarSaveData : ObservableModel
{
  public sealed override FlagvarSaveData UnderlyingData { get; }
  public ReactiveProperty<int> Var { get; }

  public ObservableFlagvarSaveData(FlagvarSaveData flagvarSaveData) : base(flagvarSaveData)
  {
    UnderlyingData = flagvarSaveData;
    Var = ReactiveProperty.FromObject(UnderlyingData, data => data.Var);
  }
}
