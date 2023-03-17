using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfArea : BfSerializableNamedId
{
  public override IList<string> Names => BugFablesLib.Names.Names.s_areas;
}
