using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfMap : BfSerializableResource
{
  public override IReadOnlyList<string> AllNames => Names.s_maps.AsReadOnly();
}
