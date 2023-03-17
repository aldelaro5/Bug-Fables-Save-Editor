using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfMedal : BfSerializableNamedId
{
  public override IList<string> Names => BugFablesLib.Names.Names.s_medals;
}
