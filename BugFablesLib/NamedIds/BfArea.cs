using System.Collections.Generic;

namespace BugFablesLib.NamedIds;

public class BfArea : BfNamedIdData
{
  public override IList<string> Names => BugFablesLib.Names.s_areas;
}
