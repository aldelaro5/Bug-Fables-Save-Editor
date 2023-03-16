using System.Collections.Generic;

namespace BugFablesLib.NamedIds;

public class BfQuest : BfNamedIdData
{
  public override IList<string> Names => BugFablesLib.Names.s_quests;
}
