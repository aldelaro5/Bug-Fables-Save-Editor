using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BugFablesSaveEditor.Enums;

namespace BugFablesSaveEditor.BugFablesSave.Sections;

public sealed class Library : BugFablesDataList<Library.LibrarySectionInfo>
{
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

  public sealed class LibrarySectionInfo : BugFablesDataList<LibraryFlag>
  {
    public override void Parse(string str)
    {
      base.Parse(str);
      for (int i = 0; i < List.Count; i++)
        List[i].Index = i;
    }
  }

  public sealed class LibraryFlag : BugFablesData, INotifyPropertyChanged
  {
    private bool _enabled;
    private int _index;

    public int Index
    {
      get => _index;
      set
      {
        _index = value;
        NotifyPropertyChanged();
      }
    }

    public bool Enabled
    {
      get => _enabled;
      set
      {
        _enabled = value;
        NotifyPropertyChanged();
      }
    }

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

    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
