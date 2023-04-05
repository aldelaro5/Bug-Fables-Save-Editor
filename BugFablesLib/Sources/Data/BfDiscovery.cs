using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfDiscovery : BfSerializableNamedId
{
  internal override IReadOnlyList<string> VanillaNames { get => BfVanillaNames.Discoveries; }
}
