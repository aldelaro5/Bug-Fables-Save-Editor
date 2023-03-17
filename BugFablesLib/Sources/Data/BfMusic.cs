using System.Collections.Generic;

namespace BugFablesLib.Data;

public class BfMusic : BfNamedIdData
{
  public override IList<string> Names => BugFablesLib.Names.Names.s_musics;
}
