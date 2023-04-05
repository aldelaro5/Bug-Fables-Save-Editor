using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfMap : BfSerializableNamedId
{
  internal override IReadOnlyList<string> VanillaNames { get => BfVanillaNames.Maps; }
}
