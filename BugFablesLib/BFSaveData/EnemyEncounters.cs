using System;
using System.Text;
using static BugFablesLib.Utils;

namespace BugFablesLib.BFSaveData;

public sealed class EnemyEncounter : IBfData
{
  public int NbrDefeated { get; set; }
  public int NbrSeen { get; set; }

  public void Deserialize(string str)
  {
    string[] data = str.Split(new[] { CommaSeparator }, StringSplitOptions.None);
    if (data.Length != 2)
      throw new Exception($"Expected 2 fields, but got {data.Length}");

    NbrSeen = ParseValueType<int>(data[0], nameof(NbrSeen));
    NbrDefeated = ParseValueType<int>(data[1], nameof(NbrDefeated));
  }

  public string Serialize()
  {
    StringBuilder sb = new();

    sb.Append(NbrSeen);
    sb.Append(CommaSeparator);
    sb.Append(NbrDefeated);

    return sb.ToString();
  }

  public void ResetToDefault()
  {
    NbrDefeated = 0;
    NbrSeen = 0;
  }
}
