using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfItem : BfSerializableResource
{
  public override IReadOnlyList<string> AllNames => Names.s_items.AsReadOnly();
}
