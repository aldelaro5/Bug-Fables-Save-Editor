using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfMedal : BfNamedIdData
{
  public override IList<string> Names => BugFablesLib.Names.Names.s_medals;
}
