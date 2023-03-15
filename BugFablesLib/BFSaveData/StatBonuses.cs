using System;
using System.Text;
using static BugFablesLib.Utils;

namespace BugFablesLib.BFSaveData;

public sealed class StatBonus : BfData
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
  public AnimId Target { get; set; } = new();

  public override void Deserialize(string str)
  {
    string[] data = str.Split(new[] { CommaSeparator }, StringSplitOptions.None);

    Type = (StatBonusType)ParseValueType<int>(data[0], nameof(Type));
    Amount = ParseValueType<int>(data[1], nameof(Amount));
    Target.Deserialize(data[2]);
  }

  public override string Serialize()
  {
    StringBuilder sb = new();

    sb.Append((int)Type);
    sb.Append(CommaSeparator);
    sb.Append(Amount);
    sb.Append(CommaSeparator);
    sb.Append(Target.Serialize());

    return sb.ToString();
  }

  public override void ResetToDefault()
  {
    Type = 0;
    Amount = 0;
    Target.ResetToDefault();
  }
}
