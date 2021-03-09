using BugFablesSaveEditor.BugFablesEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class StatBonuses : IBugFablesSaveSection
  {
    public class StatBonusInfo
    {
      public StatBonusType Type { get; set; }
      public int Amount { get; set; }
      public StatBonusTarget Target { get; set; }
    }

    public object Data { get; set; } = new List<StatBonusInfo>();

    public void ParseFromSaveLine(string saveLine)
    {
      string[] statsBonusesData = saveLine.Split(Common.ElementSeparator);
      List<StatBonusInfo> statBonuses = (List<StatBonusInfo>)Data;

      for (int i = 0; i < statsBonusesData.Length; i++)
      {
        if (statsBonusesData[i] == string.Empty)
          continue;

        string[] data = statsBonusesData[i].Split(Common.FieldSeparator);

        StatBonusInfo newStatBonus = new StatBonusInfo();

        int intOut = 0;
        if (!int.TryParse(data[0], out intOut))
          throw new Exception(nameof(StatBonuses) + "[" + i + "]." + nameof(StatBonusInfo.Type) + " failed to parse");
        if (intOut < 0 || intOut >= (int)StatBonusType.COUNT)
          throw new Exception(nameof(StatBonuses) + "[" + i + "]." + nameof(StatBonusInfo.Type) + ": " + intOut + " is not a valid stat bonus type ID");
        newStatBonus.Type = (StatBonusType)intOut;
        if (!int.TryParse(data[1], out intOut))
          throw new Exception(nameof(StatBonuses) + "[" + i + "]." + nameof(StatBonusInfo.Amount) + " failed to parse");
        newStatBonus.Amount = intOut;
        if (!int.TryParse(data[2], out intOut))
          throw new Exception(nameof(StatBonuses) + "[" + i + "]." + nameof(StatBonusInfo.Target) + " failed to parse");
        if (intOut < -1 || intOut >= (int)StatBonusTarget.COUNT)
          throw new Exception(nameof(StatBonuses) + "[" + i + "]." + nameof(StatBonusInfo.Target) + ": " + intOut + " is not a valid stat bonus target value");
        newStatBonus.Target = (StatBonusTarget)intOut;

        statBonuses.Add(newStatBonus);
      }
    }

    public string EncodeToSaveLine()
    {
      List<StatBonusInfo> statBonuses = (List<StatBonusInfo>)Data;
      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < statBonuses.Count; i++)
      {
        sb.Append((int)statBonuses[i].Type);
        sb.Append(Common.FieldSeparator);
        sb.Append(statBonuses[i].Amount);
        sb.Append(Common.FieldSeparator);
        sb.Append((int)statBonuses[i].Target);

        if (i != statBonuses.Count - 1)
          sb.Append(Common.ElementSeparator);
      }

      return sb.ToString();
    }
  }
}
