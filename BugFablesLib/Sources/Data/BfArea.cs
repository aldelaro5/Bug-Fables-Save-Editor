using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfArea : BfSerializableNamedId
{
  internal override IReadOnlyList<string> VanillaNames { get => BfVanillaNames.Areas; }
}
