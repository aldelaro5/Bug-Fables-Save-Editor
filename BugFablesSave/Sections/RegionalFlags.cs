using System;
using System.Text;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class RegionalFlags : IBugFablesSaveSection
  {
    private const int NbrSlots = 100;

    public object Data { get; set; } = new bool[NbrSlots];

    public string EncodeToSaveLine()
    {
      bool[] regionals = (bool[])Data;
      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < regionals.Length; i++)
      {
        sb.Append(regionals[i]);

        if (i != regionals.Length - 1)
          sb.Append(Common.FieldSeparator);
      }

      return sb.ToString();
    }

    public void ParseFromSaveLine(string saveLine)
    {
      string[] regionalFlagsData = saveLine.Split(Common.FieldSeparator);
      if (regionalFlagsData.Length != NbrSlots)
        throw new Exception(nameof(RegionalFlags) + " is in an invalid format");

      bool[] regionalFlags = (bool[])Data;

      for (int i = 0; i < regionalFlagsData.Length; i++)
      {
        bool boolOut = false;
        if (!bool.TryParse(regionalFlagsData[i], out boolOut))
          throw new Exception(nameof(RegionalFlags) + "[" + i + "] failed to parse");
        regionalFlags[i] = boolOut;
      }
    }
  }
}
