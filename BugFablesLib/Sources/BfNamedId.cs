using System.Collections.Generic;

namespace BugFablesLib;

public abstract class BfNamedId
{
  public int Id { get; set; }

  public string Name
  {
    get
    {
      if (Id >= VanillaNames.Count)
        return Id.ToString();

      return Id >= 0 ? VanillaNames[Id] : "INVALID";
    }
  }

  internal abstract IReadOnlyList<string> VanillaNames { get; }
}
