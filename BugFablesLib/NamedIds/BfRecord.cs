using System.Collections.Generic;

namespace BugFablesLib.NamedIds;

public class BfRecord : BfNamedIdData
{
  public override IList<string> Names => BugFablesLib.Names.s_records;
}
