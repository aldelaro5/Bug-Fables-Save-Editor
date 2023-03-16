using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfQuest : BfNamedIdData
{
  public override IList<string> Names => BugFablesLib.Names.s_quests;
}
