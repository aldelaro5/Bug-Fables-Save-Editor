using System.Collections.Generic;

namespace BugFablesLib;

public abstract class BfNamedId
{
  public int Id { get; set; }
  public string Name { get => Id >= VanillaNames.Count ? Id.ToString() : VanillaNames[Id]; }
  internal abstract IReadOnlyList<string> VanillaNames { get; }
}
