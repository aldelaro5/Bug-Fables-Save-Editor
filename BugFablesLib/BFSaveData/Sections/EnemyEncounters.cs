using System;
using System.Text;

namespace BugFablesLib.BFSaveData.Sections;

public sealed class EnemyEncounters : BfDataList<EnemyEncounters.EnemyEncounterInfo>
{
  public EnemyEncounters()
  {
    ElementSeparator = Utils.SecondarySeparator;
  }

  public sealed class EnemyEncounterInfo : BfData
  {
    public int NbrDefeated { get; set; }
    public int NbrSeen { get; set; }

    public override void ResetToDefault()
    {
      NbrDefeated = 0;
      NbrSeen = 0;
    }

    public override void Parse(string str)
    {
      string[] data = str.Split(new[] {Utils.PrimarySeparator}, StringSplitOptions.None);
      if (data.Length != 2)
        throw new Exception($"Expected 2 fields, but got {data.Length}");

      NbrSeen = ParseField<int>(data[0], nameof(NbrSeen));
      NbrDefeated = ParseField<int>(data[1], nameof(NbrDefeated));
    }

    public override string ToString()
    {
      StringBuilder sb = new();

      sb.Append(NbrSeen);
      sb.Append(Utils.PrimarySeparator);
      sb.Append(NbrDefeated);

      return sb.ToString();
    }
  }
}
