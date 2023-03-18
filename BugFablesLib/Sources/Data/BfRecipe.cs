using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfRecipe : BfSerializableResource
{
  public override IReadOnlyList<string> AllNames => Names.s_recipes.AsReadOnly();
}
