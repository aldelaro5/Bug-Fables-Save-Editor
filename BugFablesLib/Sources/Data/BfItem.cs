using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfItem : BfSerializableNamedId
{
  internal override IReadOnlyList<string> VanillaNames { get => BfVanillaNames.Items; }
}
