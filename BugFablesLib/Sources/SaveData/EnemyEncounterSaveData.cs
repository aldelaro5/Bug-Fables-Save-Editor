using System;
using System.Text;

namespace BugFablesLib.SaveData;

public sealed class EnemyEncounterSaveData : IBfSerializable
{
  public int NbrDefeated { get; set; }
  public int NbrSeen { get; set; }

  public void Deserialize(string str)
  {
    string[] data = str.Split(new[] { Utils.CommaSeparator }, StringSplitOptions.None);
    if (data.Length != 2)
      throw new Exception($"Expected 2 fields, but got {data.Length}");

    NbrSeen = Utils.ParseValueType<int>(data[0], nameof(NbrSeen));
    NbrDefeated = Utils.ParseValueType<int>(data[1], nameof(NbrDefeated));
  }

  public string Serialize()
  {
    StringBuilder sb = new();

    sb.Append(NbrSeen);
    sb.Append(Utils.CommaSeparator);
    sb.Append(NbrDefeated);

    return sb.ToString();
  }
}
