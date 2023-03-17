using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfQuest : BfSerializableNamedId
{
  public override IList<string> Names => BugFablesLib.Names.Names.s_quests;
}
