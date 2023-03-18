using System.Collections.Generic;

namespace BugFablesLib;

public abstract class BfResource
{
  public abstract IReadOnlyList<string> AllNames { get; }
  public int Id { get; set; }
  public string Name => Id >= AllNames.Count ? Id.ToString() : AllNames[Id];
}
