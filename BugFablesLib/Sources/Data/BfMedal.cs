using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfMedal : BfSerializableNamedId
{
  internal override IReadOnlyList<string> VanillaNames { get => BfVanillaNames.Medals; }
}
