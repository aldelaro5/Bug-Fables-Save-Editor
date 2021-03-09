using BugFablesSaveEditor.BugFablesEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class Library : IBugFablesSaveSection
  {
    private const int nbrSlotsPerSection = 256;

    public object Data { get; set; } = new bool[(int)LibrarySection.COUNT, nbrSlotsPerSection];

    public string EncodeToSaveLine()
    {
      bool[,] flags = (bool[,])Data;
      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < (int)LibrarySection.COUNT; i++)
      {
        for (int j = 0; j < nbrSlotsPerSection; j++)
        {
          sb.Append(flags[i, j]);

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

      bool[,] flags = (bool[,])Data;

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
          flags[i, j] = boolOut;
        }
      }
    }
  }
}
