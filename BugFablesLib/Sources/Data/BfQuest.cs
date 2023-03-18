using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfQuest : BfSerializableResource
{
  public override IReadOnlyList<string> AllNames => Names.s_quests.AsReadOnly();
}
