using System;
using System.Text;
using BugFablesLib.Data;

namespace BugFablesLib.SaveData;

public sealed class BfMedalOnHandSaveData : IBfSerializable
{
  public BfMedal Medal { get; set; } = new();
  public int MedalEquipTarget { get; set; }

  public void Deserialize(string str)
  {
    string[] data = str.Split(new[] { Utils.CommaSeparator }, StringSplitOptions.None);

    Medal.Id = Utils.ParseValueType<int>(data[0], nameof(Medal));
    MedalEquipTarget = Utils.ParseValueType<int>(data[1], nameof(MedalEquipTarget));
  }

  public string Serialize()
  {
    StringBuilder sb = new();

    sb.Append(Medal.Id);
    sb.Append(Utils.CommaSeparator);
    sb.Append(MedalEquipTarget);

    return sb.ToString();
  }

  public void ResetToDefault()
  {
    Medal.Id = 0;
    MedalEquipTarget = 0;
  }
}
