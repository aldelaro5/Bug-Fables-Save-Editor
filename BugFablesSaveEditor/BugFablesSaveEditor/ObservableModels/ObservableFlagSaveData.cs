using BugFablesLib.SaveData;
using Reactive.Bindings;

namespace BugFablesSaveEditor.ObservableModels;

public class ObservableFlagSaveData : ObservableModel
{
  public sealed override FlagSaveData UnderlyingData { get; }
  public ReactiveProperty<bool> Enabled { get; }

  public ObservableFlagSaveData(FlagSaveData flagSaveData) : base(flagSaveData)
  {
    UnderlyingData = flagSaveData;
    Enabled = ReactiveProperty.FromObject(UnderlyingData, data => data.Enabled);
  }
}
