using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfEnemy : BfSerializableResource
{
  public override IReadOnlyList<string> AllNames => Names.s_enemies.AsReadOnly();
}
