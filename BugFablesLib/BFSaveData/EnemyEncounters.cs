using System;
using System.Text;
using static BugFablesLib.Utils;

namespace BugFablesLib.BFSaveData;

public sealed class EnemyEncounter : BfData
{
  public int NbrDefeated { get; set; }
  public int NbrSeen { get; set; }

  public override void Deserialize(string str)
  {
    string[] data = str.Split(new[] { CommaSeparator }, StringSplitOptions.None);
    if (data.Length != 2)
      throw new Exception($"Expected 2 fields, but got {data.Length}");

    NbrSeen = ParseValueType<int>(data[0], nameof(NbrSeen));
    NbrDefeated = ParseValueType<int>(data[1], nameof(NbrDefeated));
  }

  public override string Serialize()
  {
    StringBuilder sb = new();

    sb.Append(NbrSeen);
    sb.Append(CommaSeparator);
    sb.Append(NbrDefeated);

    return sb.ToString();
  }

  public override void ResetToDefault()
  {
    NbrDefeated = 0;
    NbrSeen = 0;
  }
}
