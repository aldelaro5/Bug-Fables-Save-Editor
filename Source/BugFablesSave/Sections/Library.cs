using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using BugFablesSaveEditor.BugFablesEnums;

namespace BugFablesSaveEditor.BugFablesSave.Sections;

public class Library : IBugFablesSaveSection
{
  private const int nbrSlotsPerSection = 256;

  public Library()
  {
    LibraryFlag[][] array = (LibraryFlag[][])Data;
    for (int i = 0; i < (int)LibrarySection.COUNT; i++)
    {
      array[i] = new LibraryFlag[nbrSlotsPerSection];
      for (int j = 0; j < array[i].Length; j++)
      {
        array[i][j] = new LibraryFlag { Index = j };
      }
    }
  }

  public object Data { get; set; } = new LibraryFlag[(int)LibrarySection.COUNT][];

  public string EncodeToSaveLine()
  {
    LibraryFlag[][] flags = (LibraryFlag[][])Data;
    StringBuilder sb = new();

    for (int i = 0; i < (int)LibrarySection.COUNT; i++)
    {
      for (int j = 0; j < nbrSlotsPerSection; j++)
      {
        sb.Append(flags[i][j].Enabled);

        if (j != nbrSlotsPerSection - 1)
        {
          sb.Append(Common.FieldSeparator);
        }
      }

      if (i != (int)LibrarySection.COUNT - 1)
      {
        sb.Append(Common.ElementSeparator);
      }
    }

    return sb.ToString();
  }

  public void ParseFromSaveLine(string saveLine)
  {
    string[] libraryData = saveLine.Split(Common.ElementSeparator);
    if (libraryData.Length != (int)LibrarySection.COUNT)
    {
      throw new Exception(nameof(Library) + " is in an invalid format");
    }

    LibraryFlag[][] flags = (LibraryFlag[][])Data;

    for (int i = 0; i < libraryData.Length; i++)
    {
      string[] data = libraryData[i].Split(Common.FieldSeparator);
      if (data.Length != nbrSlotsPerSection)
      {
        throw new Exception(nameof(Library) + "[" + Enum.GetNames(typeof(LibrarySection))[i] +
                            "] is in an invalid format");
      }

      bool boolOut = false;
      for (int j = 0; j < data.Length; j++)
      {
        if (!bool.TryParse(data[j], out boolOut))
        {
          throw new Exception(nameof(Library) + "[" + Enum.GetNames(typeof(LibrarySection))[i] + "][" + j +
                              "] failed to parse");
        }

        flags[i][j].Enabled = boolOut;
      }
    }
  }

  public void ResetToDefault()
  {
    LibraryFlag[][] flags = (LibraryFlag[][])Data;
    foreach (LibraryFlag[] section in flags)
    {
      foreach (LibraryFlag flag in section)
      {
        flag.Enabled = false;
      }
    }
  }

  public class LibraryFlag : INotifyPropertyChanged
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

    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
