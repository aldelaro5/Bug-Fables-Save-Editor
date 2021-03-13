using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class Flagstrings : IBugFablesSaveSection
  {
    private const int NbrSlots = 15;
    private const string separator = "|SPLIT|";

    public object Data { get; set; } = new string[NbrSlots];

    public string EncodeToSaveLine()
    {
      string[] flagstrings = (string[])Data;
      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < flagstrings.Length; i++)
      {
        sb.Append(flagstrings[i]);

        if (i != flagstrings.Length - 1)
          sb.Append(separator);
      }

      return sb.ToString();
    }

    public void ParseFromSaveLine(string saveLine)
    {
      string[] flagstringData = saveLine.Split(separator);
      if (flagstringData.Length != NbrSlots)
        throw new Exception(nameof(Flagstrings) + " is in an invalid format");

      string[] flagstrings = (string[])Data;

      for (int i = 0; i < flagstringData.Length; i++)
        flagstrings[i] = flagstringData[i];
    }
  }
}
