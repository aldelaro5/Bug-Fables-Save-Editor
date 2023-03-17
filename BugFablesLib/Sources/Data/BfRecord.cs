using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfRecord : BfNamedIdData
{
  public override IList<string> Names => BugFablesLib.Names.Names.s_records;
}
