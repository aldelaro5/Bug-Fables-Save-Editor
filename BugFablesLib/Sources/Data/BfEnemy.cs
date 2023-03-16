using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfEnemy : BfNamedIdData
{
  public override IList<string> Names => BugFablesLib.Names.s_enemies;
}
