using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfRecipe : BfNamedIdData
{
  public override IList<string> Names => BugFablesLib.Names.Names.s_recipes;
}
