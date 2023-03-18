using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfMedal : BfSerializableResource
{
  public override IReadOnlyList<string> AllNames => Names.s_medals.AsReadOnly();
}
