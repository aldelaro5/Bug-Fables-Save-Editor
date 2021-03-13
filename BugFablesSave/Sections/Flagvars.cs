using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class Flagvars : IBugFablesSaveSection
  {
    private const int NbrSlots = 70;

    public object Data { get; set; } = new int[NbrSlots];

    public string EncodeToSaveLine()
    {
      int[] flagvars = (int[])Data;
      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < flagvars.Length; i++)
      {
        sb.Append(flagvars[i]);

        if (i != flagvars.Length - 1)
          sb.Append(Common.FieldSeparator);
      }

      return sb.ToString();
    }

    public void ParseFromSaveLine(string saveLine)
    {
      string[] flagsvarsData = saveLine.Split(Common.FieldSeparator);
      if (flagsvarsData.Length != NbrSlots)
        throw new Exception(nameof(Flagvars) + " is in an invalid format");

      int[] flagsvars = (int[])Data;

      for (int i = 0; i < flagsvarsData.Length; i++)
      {
        int intOut = 0;
        if (!int.TryParse(flagsvarsData[i], out intOut))
          throw new Exception(nameof(Flagvars) + "[" + i + "] failed to parse");
        flagsvars[i] = intOut;
      }
    }
  }
}
