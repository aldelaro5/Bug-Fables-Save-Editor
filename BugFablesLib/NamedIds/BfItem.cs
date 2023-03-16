using System.Collections.Generic;

namespace BugFablesLib.NamedIds;

public class BfItem : BfNamedIdData
{
  public override IList<string> Names => BugFablesLib.Names.s_items;
}
