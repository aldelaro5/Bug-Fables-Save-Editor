using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfDiscovery : BfNamedIdData
{
  public override IList<string> Names => BugFablesLib.Names.Names.s_discoveries;
}
