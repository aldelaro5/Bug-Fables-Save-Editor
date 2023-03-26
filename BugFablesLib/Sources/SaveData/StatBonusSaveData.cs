using System;
using System.Text;
using BugFablesLib.Data;

namespace BugFablesLib.SaveData;

public sealed class StatBonusSaveData : IBfSerializable
{
  public enum StatBonusType
  {
    HP = 0,
    Attack,
    Defense,
    TP,
    MP
  }

  public StatBonusType Type { get; set; }
  public int Amount { get; set; }
  public BfAnimId Target { get; } = new();

  public void Deserialize(string str)
  {
    string[] data = str.Split(new[] { Utils.CommaSeparator }, StringSplitOptions.None);

    Type = (StatBonusType)Utils.ParseValueType<int>(data[0], nameof(Type));
    Amount = Utils.ParseValueType<int>(data[1], nameof(Amount));
    Target.Id = Utils.ParseValueType<int>(data[2], nameof(Target));
  }

  public string Serialize()
  {
    StringBuilder sb = new();

    sb.Append((int)Type);
    sb.Append(Utils.CommaSeparator);
    sb.Append(Amount);
    sb.Append(Utils.CommaSeparator);
    sb.Append(Target.Id);

    return sb.ToString();
  }

  public void ResetToDefault()
  {
    Type = 0;
    Amount = 0;
    Target.Id = 0;
  }
}
