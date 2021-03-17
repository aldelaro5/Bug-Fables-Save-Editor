using BugFablesSaveEditor.BugFablesEnums;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class StatBonuses : IBugFablesSaveSection
  {
    public class StatBonusInfo : INotifyPropertyChanged
    {
      private StatBonusType _type;
      public StatBonusType Type
      {
        get
        {
          return _type;
        }
        set
        {
          // This workaround an issue where changing tabs sets this to -1???
          if ((int)value == -1)
            return;
          _type = value;
          NotifyPropertyChanged();
        }
      }

      private int _amount;
      public int Amount { get { return _amount; } set { _amount = value; NotifyPropertyChanged(); } }

      private StatBonusTarget _target;
      public StatBonusTarget Target { get { return _target; } set { _target = value; NotifyPropertyChanged(); } }

      public event PropertyChangedEventHandler? PropertyChanged;
      private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
      {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    public object Data { get; set; } = new ObservableCollection<StatBonusInfo>();

    /// <summary>
    /// Get the total amount of bonuses for a given type and target, -1 for party only
    /// </summary>
    /// <param name="target">The target as int</param>
    /// <param name="type">The bonus type</param>
    /// <returns>The sum of the total bonus amount</returns>
    public int GetTotalBonusesForTargetAndType(int target, StatBonusType type)
    {
      ObservableCollection<StatBonusInfo> statBonuses = (ObservableCollection<StatBonusInfo>)Data;
      if (target == -1)
      {
        return statBonuses.Where(x => x.Target == StatBonusTarget.Party && x.Type == type)
                          .Sum(x => x.Amount);
      }
      else
      {
        return statBonuses.Where(x => ((int)x.Target == target || x.Target == StatBonusTarget.Party) && x.Type == type)
                          .Sum(x => x.Amount);
      }
    }

    public void ParseFromSaveLine(string saveLine)
    {
      string[] statsBonusesData = saveLine.Split(Common.ElementSeparator);
      ObservableCollection<StatBonusInfo> statBonuses = (ObservableCollection<StatBonusInfo>)Data;

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
      ObservableCollection<StatBonusInfo> statBonuses = (ObservableCollection<StatBonusInfo>)Data;
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
