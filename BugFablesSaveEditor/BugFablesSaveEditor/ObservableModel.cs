using BugFablesLib;

namespace BugFablesSaveEditor;

public abstract class ObservableModel
{
  protected ObservableModel(IBfSerializable underlyingData) => UnderlyingData = underlyingData;
  public virtual IBfSerializable UnderlyingData { get; }
}
