using System;
using System.Text;

namespace BugFablesLib.BFSaveData.Sections;

public sealed class StatBonuses : BfDataList<StatBonuses.StatBonusInfo>
{
  public StatBonuses()
  {
    ElementSeparator = Utils.SecondarySeparator;
  }

  public sealed class StatBonusInfo : BfData
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
    public int Target { get; set; }

    public override void ResetToDefault()
    {
      Type = 0;
      Amount = 0;
      Target = 0;
    }

    public override void Parse(string str)
    {
      string[] data = str.Split(new[] {Utils.PrimarySeparator}, StringSplitOptions.None);

      Type = (StatBonusType)ParseField<int>(data[0], nameof(Type));
      Amount = ParseField<int>(data[1], nameof(Amount));
      Target = ParseField<int>(data[2], nameof(Target));
    }

    public override string ToString()
    {
      StringBuilder sb = new();

      sb.Append((int)Type);
      sb.Append(Utils.PrimarySeparator);
      sb.Append(Amount);
      sb.Append(Utils.PrimarySeparator);
      sb.Append(Target);

      return sb.ToString();
    }
  }
}
