using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfMusic : BfSerializableResource
{
  public override IReadOnlyList<string> AllNames => Names.s_musics.AsReadOnly();
}
