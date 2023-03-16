using System.Collections.Generic;

namespace BugFablesLib;

public abstract class BfNamedId
{
  public abstract IList<string> Names { get; }
  public int Id { get; set; }
  public string Name => Id >= Names.Count ? Id.ToString() : Names[Id];
}
