using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfArea : BfSerializableResource
{
  public override IReadOnlyList<string> AllNames => Names.s_areas.AsReadOnly();
}
