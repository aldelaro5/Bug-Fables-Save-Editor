using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfItem : BfSerializableNamedId
{
  public override IList<string> Names => BugFablesLib.Names.Names.s_items;
}
