using System;
using System.Text;
using BugFablesLib.Data;
using static BugFablesLib.Utils;

namespace BugFablesLib.SaveData;

public sealed class MedalOnHandSaveData : IBfData
{
  public BfMedal Medal { get; set; } = new();
  public int MedalEquipTarget { get; set; }

  public void Deserialize(string str)
  {
    string[] data = str.Split(new[] { CommaSeparator }, StringSplitOptions.None);

    Medal.Id = ParseValueType<int>(data[0], nameof(Medal));
    MedalEquipTarget = ParseValueType<int>(data[1], nameof(MedalEquipTarget));
  }

  public string Serialize()
  {
    StringBuilder sb = new();

    sb.Append(Medal.Id);
    sb.Append(CommaSeparator);
    sb.Append(MedalEquipTarget);

    return sb.ToString();
  }

  public void ResetToDefault()
  {
    Medal.Id = 0;
    MedalEquipTarget = 0;
  }
}
