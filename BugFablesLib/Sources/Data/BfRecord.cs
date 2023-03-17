using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfRecord : BfSerializableNamedId
{
  public override IList<string> Names => BugFablesLib.Names.Names.s_records;
}
