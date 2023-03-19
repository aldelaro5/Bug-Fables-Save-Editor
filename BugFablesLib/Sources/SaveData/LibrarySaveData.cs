using static BugFablesLib.Utils;

namespace BugFablesLib.SaveData;

public sealed class LibrarySaveData : BfSerializableCollection<BfSerializableCollection<FlagSaveData>>
{
  public enum LibrarySection
  {
    Discovery = 0,
    Bestiary,
    Recipe,
    Record,
    SeenAreas
  }

  public BfSerializableCollection<FlagSaveData> Discoveries { get => this[(int)LibrarySection.Discovery]; }
  public BfSerializableCollection<FlagSaveData> Bestiary { get => this[(int)LibrarySection.Bestiary]; }
  public BfSerializableCollection<FlagSaveData> Recipes { get => this[(int)LibrarySection.Recipe]; }
  public BfSerializableCollection<FlagSaveData> Records { get => this[(int)LibrarySection.Record]; }
  public BfSerializableCollection<FlagSaveData> SeenAreas { get => this[(int)LibrarySection.SeenAreas]; }

  public LibrarySaveData()
  {
    NbrExpectedElements = 5;
    for (int i = 0; i < NbrExpectedElements; i++)
      Add(new BfSerializableCollection<FlagSaveData>(CommaSeparator));
  }
}
