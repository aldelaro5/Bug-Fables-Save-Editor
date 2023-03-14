using System.Collections.Generic;

namespace BugFablesLib.BFSaveData.Sections;

public sealed class Library : BfDataList<Library.LibrarySectionInfo>
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

  public IList<LibraryFlag> Discoveries { get => List[(int)LibrarySection.Discovery].List; }
  public IList<LibraryFlag> Bestiary { get => List[(int)LibrarySection.Bestiary].List; }
  public IList<LibraryFlag> Recipes { get => List[(int)LibrarySection.Recipe].List; }
  public IList<LibraryFlag> Records { get => List[(int)LibrarySection.Record].List; }
  public IList<LibraryFlag> SeenAreas { get => List[(int)LibrarySection.SeenAreas].List; }

  public Library()
  {
    ElementSeparator = Utils.SecondarySeparator;
    NbrExpectedElements = (int)LibrarySection.COUNT;
    while (List.Count < (int)LibrarySection.COUNT)
      List.Add(new LibrarySectionInfo());
  }

  public sealed class LibrarySectionInfo : BfDataList<LibraryFlag>
  {
  }

  public sealed class LibraryFlag : BfData
  {
    public bool Enabled { get; set; }

    public override void ResetToDefault()
    {
      Enabled = false;
    }

    public override void Parse(string str)
    {
      Enabled = ParseField<bool>(str, nameof(Enabled));
    }

    public override string ToString()
    {
      return Enabled.ToString();
    }
  }
}
