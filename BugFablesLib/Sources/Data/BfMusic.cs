using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfMusic : BfSerializableNamedId
{
  internal override IReadOnlyList<string> VanillaNames { get => BfVanillaNames.Musics; }
}
