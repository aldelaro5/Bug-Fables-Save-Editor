using BugFablesLib;

namespace BugFablesSaveEditor;

public abstract class BfObservable
{
  protected BfObservable(IBfSerializable underlyingData) => UnderlyingData = underlyingData;
  public virtual IBfSerializable UnderlyingData { get; }
}
