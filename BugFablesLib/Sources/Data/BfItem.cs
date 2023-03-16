using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfItem : BfNamedIdData
{
  public override IList<string> Names => BugFablesLib.Names.s_items;
}
