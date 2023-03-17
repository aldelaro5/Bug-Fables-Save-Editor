using static BugFablesLib.Utils;

namespace BugFablesLib.SaveData;

public sealed class LibrarySaveData : BfDataCollection<BfDataCollection<LibraryFlag>>
{
  public enum LibrarySection
  {
    Discovery = 0,
    Bestiary,
    Recipe,
    Record,
    SeenAreas
  }

  public BfDataCollection<LibraryFlag> Discoveries { get => this[(int)LibrarySection.Discovery]; }
  public BfDataCollection<LibraryFlag> Bestiary { get => this[(int)LibrarySection.Bestiary]; }
  public BfDataCollection<LibraryFlag> Recipes { get => this[(int)LibrarySection.Recipe]; }
  public BfDataCollection<LibraryFlag> Records { get => this[(int)LibrarySection.Record]; }
  public BfDataCollection<LibraryFlag> SeenAreas { get => this[(int)LibrarySection.SeenAreas]; }

  public LibrarySaveData()
  {
    NbrExpectedElements = 5;
    for (int i = 0; i < NbrExpectedElements; i++)
      Add(new BfDataCollection<LibraryFlag>(CommaSeparator));
  }
}

public sealed class LibraryFlag : IBfData
{
  public bool Enabled { get; set; }

  public void Deserialize(string str) =>
    Enabled = ParseValueType<bool>(str, nameof(Enabled));

  public string Serialize() => Enabled.ToString();
  public void ResetToDefault() => Enabled = false;
}
