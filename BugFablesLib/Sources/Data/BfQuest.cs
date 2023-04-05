using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfQuest : BfSerializableNamedId
{
  internal override IReadOnlyList<string> VanillaNames { get => BfVanillaNames.Quests; }
}
