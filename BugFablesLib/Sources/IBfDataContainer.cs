using System.Collections.Generic;

namespace BugFablesLib;

public interface IBfDataContainer
{
  public IList<IBfSerializable> Data { get; }
}
