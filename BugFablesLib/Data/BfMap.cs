using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfMap : BfNamedIdData
{
  public override IList<string> Names => BugFablesLib.Names.s_maps;
}
