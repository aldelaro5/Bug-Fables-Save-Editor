using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfAnimId : BfSerializableResource
{
  public override IReadOnlyList<string> AllNames => Names.s_animIds.AsReadOnly();
}
