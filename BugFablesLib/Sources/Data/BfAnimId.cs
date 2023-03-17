using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfAnimId : BfNamedIdData
{
  public override IList<string> Names => BugFablesLib.Names.Names.AnimIds;
}
