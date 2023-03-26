using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfRecipe : BfSerializableNamedId
{
  internal override IReadOnlyList<string> VanillaNames { get => BfVanillaNames.Recipes; }
}
