using BugFablesSaveEditor.BugFablesEnums;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class Library : IBugFablesSaveSection
  {
    public class LibraryFlag : INotifyPropertyChanged
    {
      private int _index;
      public int Index
      {
        get { return _index; }
        set { _index = value; NotifyPropertyChanged(); }
      }

      private bool _enabled;
      public bool Enabled
      {
        get { return _enabled; }
        set { _enabled = value; NotifyPropertyChanged(); }
      }

      public event PropertyChangedEventHandler? PropertyChanged;
      private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
      {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    private const int nbrSlotsPerSection = 256;

    public object Data { get; set; } = new LibraryFlag[(int)LibrarySection.COUNT][];

    public Library()
    {
      var array = (LibraryFlag[][])Data;
      for (int i = 0; i < (int)LibrarySection.COUNT; i++)
      {
        array[i] = new LibraryFlag[nbrSlotsPerSection];
        for (int j = 0; j < array[i].Length; j++)
        {
          array[i][j] = new LibraryFlag { Index = j };
        }
      }
    }

    public string EncodeToSaveLine()
    {
      LibraryFlag[][] flags = (LibraryFlag[][])Data;
      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < (int)LibrarySection.COUNT; i++)
      {
        for (int j = 0; j < nbrSlotsPerSection; j++)
        {
          sb.Append(flags[i][j].Enabled);

          if (j != nbrSlotsPerSection - 1)
            sb.Append(Common.FieldSeparator);
        }

        if (i != (int)LibrarySection.COUNT - 1)
          sb.Append(Common.ElementSeparator);
      }

      return sb.ToString();
    }

    public void ParseFromSaveLine(string saveLine)
    {
      string[] libraryData = saveLine.Split(Common.ElementSeparator);
      if (libraryData.Length != (int)LibrarySection.COUNT)
        throw new Exception(nameof(Library) + " is in an invalid format");

      LibraryFlag[][] flags = (LibraryFlag[][])Data;

      for (int i = 0; i < libraryData.Length; i++)
      {
        string[] data = libraryData[i].Split(Common.FieldSeparator);
        if (data.Length != nbrSlotsPerSection)
          throw new Exception(nameof(Library) + "[" + Enum.GetNames(typeof(LibrarySection))[i] + "] is in an invalid format");

        bool boolOut = false;
        for (int j = 0; j < data.Length; j++)
        {
          if (!bool.TryParse(data[j], out boolOut))
            throw new Exception(nameof(Library) + "[" + Enum.GetNames(typeof(LibrarySection))[i] + "][" + j + "] failed to parse");
          flags[i][j].Enabled = boolOut;
        }
      }
    }

    public void ResetToDefault()
    {
      LibraryFlag[][] flags = (LibraryFlag[][])Data;
      foreach (var section in flags)
      {
        foreach (var flag in section)
          flag.Enabled = false;
      }
    }
  }
}
