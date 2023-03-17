using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfRecipe : BfSerializableNamedId
{
  public override IList<string> Names => BugFablesLib.Names.Names.s_recipes;
}
