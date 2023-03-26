using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfRecord : BfSerializableNamedId
{
  internal override IReadOnlyList<string> VanillaNames { get => BfVanillaNames.Records; }
}
