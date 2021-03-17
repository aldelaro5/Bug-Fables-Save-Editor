using System;
using System.Text;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class Flags : IBugFablesSaveSection
  {
    private const int NbrSlots = 750;

    public object Data { get; set; } = new bool[NbrSlots];

    public string EncodeToSaveLine()
    {
      bool[] flags = (bool[])Data;
      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < flags.Length; i++)
      {
        sb.Append(flags[i]);

        if (i != flags.Length - 1)
          sb.Append(Common.FieldSeparator);
      }

      return sb.ToString();
    }

    public void ParseFromSaveLine(string saveLine)
    {
      string[] flagsData = saveLine.Split(Common.FieldSeparator);
      if (flagsData.Length != NbrSlots)
        throw new Exception(nameof(Flags) + " is in an invalid format");

      bool[] flags = (bool[])Data;

      for (int i = 0; i < flagsData.Length; i++)
      {
        bool boolOut = false;
        if (!bool.TryParse(flagsData[i], out boolOut))
          throw new Exception(nameof(Flags) + "[" + i + "] failed to parse");
        flags[i] = boolOut;
      }
    }
  }
}
