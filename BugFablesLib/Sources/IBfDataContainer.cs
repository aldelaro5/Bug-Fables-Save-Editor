using System.Collections.Generic;

namespace BugFablesLib;

public interface IBfDataContainer
{
  public IList<IBfSerializable> Data { get; }
  public void LoadFromString(string data);
  public string EncodeToString();
  public void ResetToDefault();
}
