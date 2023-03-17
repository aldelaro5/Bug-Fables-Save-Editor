using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfArea : BfNamedIdData
{
  public override IList<string> Names => BugFablesLib.Names.Names.s_areas;
}
