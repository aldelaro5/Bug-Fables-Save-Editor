using BugFablesLib.SaveData;

namespace BugFablesSaveEditor.ObservableModels;

public class ObservableFlagvarSaveData : ObservableModel
{
  public sealed override FlagvarSaveData UnderlyingData { get; }
  public int Var
  {
    get => UnderlyingData.Var;
    set => SetProperty(UnderlyingData.Var, value, UnderlyingData, (data, s) => data.Var = s);
  }

  public ObservableFlagvarSaveData(FlagvarSaveData flagvarSaveData) : base(flagvarSaveData)
  {
    UnderlyingData = flagvarSaveData;
  }
}
