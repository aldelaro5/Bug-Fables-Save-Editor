using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfEnemy : BfSerializableNamedId
{
  public override IList<string> Names => BugFablesLib.Names.Names.s_enemies;
}
