using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfRecord : BfSerializableResource
{
  public override IReadOnlyList<string> AllNames => Names.s_records.AsReadOnly();
}
