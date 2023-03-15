using static BugFablesLib.Utils;

namespace BugFablesLib.BFSaveData;

public sealed class LibrarySections : BfDataList<BfDataList<LibraryFlag>>
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

  public BfDataList<LibraryFlag> Discoveries { get => this[(int)LibrarySection.Discovery]; }
  public BfDataList<LibraryFlag> Bestiary { get => this[(int)LibrarySection.Bestiary]; }
  public BfDataList<LibraryFlag> Recipes { get => this[(int)LibrarySection.Recipe]; }
  public BfDataList<LibraryFlag> Records { get => this[(int)LibrarySection.Record]; }
  public BfDataList<LibraryFlag> SeenAreas { get => this[(int)LibrarySection.SeenAreas]; }

  public LibrarySections()
  {
    NbrExpectedElements = (int)LibrarySection.COUNT;
    for (int i = 0; i < NbrExpectedElements; i++)
      Add(new BfDataList<LibraryFlag>(CommaSeparator));
  }
}

public sealed class LibraryFlag : BfData
{
  public bool Enabled { get; set; }

  public override void Deserialize(string str) =>
    Enabled = ParseValueType<bool>(str, nameof(Enabled));

  public override string Serialize() => Enabled.ToString();
  public override void ResetToDefault() => Enabled = false;
}
