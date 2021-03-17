using System;
using System.Text;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class CrystalBerries : IBugFablesSaveSection
  {
    private const int NbrCrystalBerries = 50;

    public object Data { get; set; } = new bool[NbrCrystalBerries];

    public string EncodeToSaveLine()
    {
      bool[] crystalBerries = (bool[])Data;
      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < crystalBerries.Length; i++)
      {
        sb.Append(crystalBerries[i]);

        if (i != crystalBerries.Length - 1)
          sb.Append(Common.FieldSeparator);
      }

      return sb.ToString();
    }

    public void ParseFromSaveLine(string saveLine)
    {
      string[] crystalBerriesData = saveLine.Split(Common.FieldSeparator);
      if (crystalBerriesData.Length != NbrCrystalBerries)
        throw new Exception(nameof(CrystalBerries) + " is in an invalid format");

      bool[] crystalBerries = (bool[])Data;

      for (int i = 0; i < crystalBerriesData.Length; i++)
      {
        bool boolOut = false;
        if (!bool.TryParse(crystalBerriesData[i], out boolOut))
          throw new Exception(nameof(CrystalBerries) + "[" + i + "] failed to parse");
        crystalBerries[i] = boolOut;
      }
    }
  }
}
