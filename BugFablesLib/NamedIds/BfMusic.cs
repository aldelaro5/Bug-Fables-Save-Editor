using System.Collections.Generic;

namespace BugFablesLib.NamedIds;

public class BfMusic : BfNamedIdData
{
  public override IList<string> Names => BugFablesLib.Names.s_musics;
}
