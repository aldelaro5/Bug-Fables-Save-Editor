using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfMap : BfSerializableNamedId
{
  public override IList<string> Names => BugFablesLib.Names.Names.s_maps;
}
