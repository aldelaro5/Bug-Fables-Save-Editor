using System;
using System.Text;

namespace BugFablesLib.BFSaveData.Sections;

public sealed class Medals : BfDataList<Medals.MedalInfo>
{
  public Medals()
  {
    ElementSeparator = Utils.SecondarySeparator;
  }

  public sealed class MedalInfo : BfData
  {
    public int Medal { get; set; }
    public int MedalEquipTarget { get; set; }

    public override void ResetToDefault()
    {
      Medal = 0;
      MedalEquipTarget = 0;
    }

    public override void Parse(string str)
    {
      string[] data = str.Split(new[] {Utils.PrimarySeparator}, StringSplitOptions.None);

      Medal = ParseField<int>(data[0], nameof(Medal));
      MedalEquipTarget = ParseField<int>(data[1], nameof(MedalEquipTarget));
    }

    public override string ToString()
    {
      StringBuilder sb = new();

      sb.Append(Medal);
      sb.Append(Utils.PrimarySeparator);
      sb.Append(MedalEquipTarget);

      return sb.ToString();
    }
  }
}
