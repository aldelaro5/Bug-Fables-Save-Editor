using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using BugFablesSaveEditor.Enums;

namespace BugFablesSaveEditor.BugFablesSave.Sections;

public sealed class StatBonuses : BugFablesDataList<StatBonuses.StatBonusInfo>
{
  public StatBonuses()
  {
    ElementSeparator = Utils.SecondarySeparator;
  }

  /// <summary>
  ///   Get the total amount of bonuses for a given type and target, -1 for party only
  /// </summary>
  /// <param name="target">The target as int</param>
  /// <param name="type">The bonus type</param>
  /// <returns>The sum of the total bonus amount</returns>
  public int GetTotalBonusesForTargetAndType(int target, StatBonusType type)
  {
    if (target == -1)
    {
      return List.Where(x => x.Target == StatBonusTarget.Party && x.Type == type)
        .Sum(x => x.Amount);
    }

    return List.Where(x =>
        ((int)x.Target == target || x.Target == StatBonusTarget.Party) && x.Type == type)
      .Sum(x => x.Amount);
  }

  public sealed class StatBonusInfo : BugFablesData, INotifyPropertyChanged
  {
    private int _amount;

    private StatBonusTarget _target;
    private StatBonusType _type;

    public StatBonusType Type
    {
      get => _type;
      set
      {
        // This workaround an issue where changing tabs sets this to -1???
        if ((int)value == -1)
        {
          return;
        }

        _type = value;
        NotifyPropertyChanged();
      }
    }

    public int Amount
    {
      get => _amount;
      set
      {
        _amount = value;
        NotifyPropertyChanged();
      }
    }

    public StatBonusTarget Target
    {
      get => _target;
      set
      {
        _target = value;
        NotifyPropertyChanged();
      }
    }

    public override void ResetToDefault()
    {
      Type = 0;
      Amount = 0;
      Target = 0;
    }

    public override void Parse(string str)
    {
      string[] data = str.Split(Utils.PrimarySeparator);

      Type = (StatBonusType)ParseField<int>(data[0], nameof(Type));
      Amount = ParseField<int>(data[1], nameof(Amount));
      Target = (StatBonusTarget)ParseField<int>(data[2], nameof(Target));
    }

    public override string ToString()
    {
      StringBuilder sb = new();

      sb.Append((int)Type);
      sb.Append(Utils.PrimarySeparator);
      sb.Append(Amount);
      sb.Append(Utils.PrimarySeparator);
      sb.Append((int)Target);

      return sb.ToString();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
