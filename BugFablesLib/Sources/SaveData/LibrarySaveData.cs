using static BugFablesLib.Utils;

namespace BugFablesLib.SaveData;

public sealed class LibrarySaveData : BfSerializableDataCollection<BfSerializableDataCollection<LibraryFlag>>
{
  public enum LibrarySection
  {
    Discovery = 0,
    Bestiary,
    Recipe,
    Record,
    SeenAreas
  }

  public BfSerializableDataCollection<LibraryFlag> Discoveries { get => this[(int)LibrarySection.Discovery]; }
  public BfSerializableDataCollection<LibraryFlag> Bestiary { get => this[(int)LibrarySection.Bestiary]; }
  public BfSerializableDataCollection<LibraryFlag> Recipes { get => this[(int)LibrarySection.Recipe]; }
  public BfSerializableDataCollection<LibraryFlag> Records { get => this[(int)LibrarySection.Record]; }
  public BfSerializableDataCollection<LibraryFlag> SeenAreas { get => this[(int)LibrarySection.SeenAreas]; }

  public LibrarySaveData()
  {
    NbrExpectedElements = 5;
    for (int i = 0; i < NbrExpectedElements; i++)
      Add(new BfSerializableDataCollection<LibraryFlag>(CommaSeparator));
  }
}

public sealed class LibraryFlag : IBfSerializable
{
  public bool Enabled { get; set; }

  public void Deserialize(string str) =>
    Enabled = ParseValueType<bool>(str, nameof(Enabled));

  public string Serialize() => Enabled.ToString();
  public void ResetToDefault() => Enabled = false;
}
