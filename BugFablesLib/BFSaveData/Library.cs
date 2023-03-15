using System.Collections.Generic;
using static BugFablesLib.Utils;

namespace BugFablesLib.BFSaveData;

public sealed class LibrarySections : BfList<BfList<LibraryFlag>>
{
  public enum LibrarySection
  {
    Discovery = 0,
    Bestiary,
    Recipe,
    Record,
    SeenAreas,
    COUNT
  }

  public IList<LibraryFlag> Discoveries { get => this[(int)LibrarySection.Discovery]; }
  public IList<LibraryFlag> Bestiary { get => this[(int)LibrarySection.Bestiary]; }
  public IList<LibraryFlag> Recipes { get => this[(int)LibrarySection.Recipe]; }
  public IList<LibraryFlag> Records { get => this[(int)LibrarySection.Record]; }
  public IList<LibraryFlag> SeenAreas { get => this[(int)LibrarySection.SeenAreas]; }

  public LibrarySections()
  {
    NbrExpectedElements = (int)LibrarySection.COUNT;
    for (int i = 0; i < NbrExpectedElements; i++)
      Add(new BfList<LibraryFlag>(CommaSeparator));
  }
}

public sealed class LibraryFlag : IBfData
{
  public bool Enabled { get; set; }
  public void Deserialize(string str) => Enabled = ParseValueType<bool>(str, nameof(Enabled));
  public string Serialize() => Enabled.ToString();
  public void ResetToDefault() => Enabled = false;
}
