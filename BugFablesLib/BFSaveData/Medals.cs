using System;
using System.Text;
using static BugFablesLib.Utils;

namespace BugFablesLib.BFSaveData;

public sealed class MedalOnHand : IBfData
{
  public Medal Medal { get; set; } = new();
  public int MedalEquipTarget { get; set; }

  public void Deserialize(string str)
  {
    string[] data = str.Split(new[] { CommaSeparator }, StringSplitOptions.None);

    Medal.Deserialize(data[0]);
    MedalEquipTarget = ParseValueType<int>(data[1], nameof(MedalEquipTarget));
  }

  public string Serialize()
  {
    StringBuilder sb = new();

    sb.Append(Medal.Serialize());
    sb.Append(CommaSeparator);
    sb.Append(MedalEquipTarget);

    return sb.ToString();
  }

  public void ResetToDefault()
  {
    Medal.ResetToDefault();
    MedalEquipTarget = 0;
  }
}
