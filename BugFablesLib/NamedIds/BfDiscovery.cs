using System.Collections.Generic;

namespace BugFablesLib.NamedIds;

public class BfDiscovery : BfNamedIdData
{
  public override IList<string> Names => BugFablesLib.Names.s_discoveries;
}
