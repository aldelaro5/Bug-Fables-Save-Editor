using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfEnemy : BfSerializableNamedId
{
  internal override IReadOnlyList<string> VanillaNames { get => BfVanillaNames.Enemies; }
}
