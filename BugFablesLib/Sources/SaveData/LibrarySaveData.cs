namespace BugFablesLib.SaveData;

public sealed class
  LibrarySaveData : BfSerializableCollection<BfSerializableCollection<FlagSaveData>>
{
  private const int NbrLibrarySectionSlots = 256;
  public enum LibrarySection
  {
    Discovery = 0,
    Bestiary,
    Recipe,
    Record,
    SeenAreas
  }

  public BfSerializableCollection<FlagSaveData> Discoveries => this[(int)LibrarySection.Discovery];
  public BfSerializableCollection<FlagSaveData> Bestiary => this[(int)LibrarySection.Bestiary];
  public BfSerializableCollection<FlagSaveData> Recipes => this[(int)LibrarySection.Recipe];
  public BfSerializableCollection<FlagSaveData> Records => this[(int)LibrarySection.Record];
  public BfSerializableCollection<FlagSaveData> SeenAreas => this[(int)LibrarySection.SeenAreas];

  public LibrarySaveData()
  {
    NbrExpectedElements = 5;
    for (int i = 0; i < NbrExpectedElements; i++)
    {
      BfSerializableCollection<FlagSaveData> newLibrary = new(Utils.CommaSeparator);
      for (int j = 0; j < NbrLibrarySectionSlots; j++)
        newLibrary.Add(new());
      Add(newLibrary);
    }
  }
}
