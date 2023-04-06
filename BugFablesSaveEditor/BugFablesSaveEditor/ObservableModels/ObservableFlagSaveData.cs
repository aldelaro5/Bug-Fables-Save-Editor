using BugFablesLib.SaveData;

namespace BugFablesSaveEditor.ObservableModels;

public class ObservableFlagSaveData : ObservableModel
{
  public sealed override FlagSaveData UnderlyingData { get; }

  public bool Enabled
  {
    get => UnderlyingData.Enabled;
    set => SetProperty(UnderlyingData.Enabled, value, UnderlyingData, (data, b) => data.Enabled = b);
  }

  public ObservableFlagSaveData(FlagSaveData flagSaveData) : base(flagSaveData)
  {
    UnderlyingData = flagSaveData;
  }
}
