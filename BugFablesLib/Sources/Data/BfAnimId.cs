using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfAnimId : BfSerializableNamedId
{
  public override IList<string> Names => BugFablesLib.Names.Names.AnimIds;
}
