using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfDiscovery : BfSerializableResource
{
  public override IReadOnlyList<string> AllNames => Names.s_discoveries.AsReadOnly();
}
