using System.Collections.Generic;

namespace BugFablesLib.NamedIds;

public class BfRecipe : BfNamedIdData
{
  public override IList<string> Names => BugFablesLib.Names.s_recipes;
}
