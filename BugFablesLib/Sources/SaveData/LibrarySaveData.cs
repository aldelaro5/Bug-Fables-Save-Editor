using static BugFablesLib.Utils;

namespace BugFablesLib.SaveData;

public sealed class LibrarySaveData : BfSerializableCollection<BfSerializableCollection<LibraryFlag>>
{
  public enum LibrarySection
  {
    Discovery = 0,
    Bestiary,
    Recipe,
    Record,
    SeenAreas
  }

  public BfSerializableCollection<LibraryFlag> Discoveries { get => this[(int)LibrarySection.Discovery]; }
  public BfSerializableCollection<LibraryFlag> Bestiary { get => this[(int)LibrarySection.Bestiary]; }
  public BfSerializableCollection<LibraryFlag> Recipes { get => this[(int)LibrarySection.Recipe]; }
  public BfSerializableCollection<LibraryFlag> Records { get => this[(int)LibrarySection.Record]; }
  public BfSerializableCollection<LibraryFlag> SeenAreas { get => this[(int)LibrarySection.SeenAreas]; }

  public LibrarySaveData()
  {
    NbrExpectedElements = 5;
    for (int i = 0; i < NbrExpectedElements; i++)
      Add(new BfSerializableCollection<LibraryFlag>(CommaSeparator));
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
