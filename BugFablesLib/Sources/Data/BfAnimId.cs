using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfAnimId : BfSerializableNamedId
{
  internal override IReadOnlyList<string> VanillaNames { get => BfVanillaNames.AnimIds; }
}
