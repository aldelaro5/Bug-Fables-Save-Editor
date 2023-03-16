using System.Collections.Generic;

namespace BugFablesLib.NamedIds;

public class BfAnimId : BfNamedIdData
{
  public override IList<string> Names => BugFablesLib.Names.AnimIds;
}
