using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using BugFablesSaveEditor.Enums;

namespace BugFablesSaveEditor.BugFablesSave.Sections;

public sealed class PartyMembers : BugFablesDataList<PartyMembers.PartyMemberInfo>
{
  public PartyMembers()
  {
    ElementSeparator = Utils.SecondarySeparator;
  }

  public class PartyMemberInfo : BugFablesData, INotifyPropertyChanged
  {
    private int _attack;

    private int _baseAttack;

    private int _baseDefense;

    private int _baseHp;

    private int _defense;

    private int _hp;

    private int _maxHp;
    private AnimID _trueid;

    public AnimID Trueid
    {
      get => _trueid;
      set
      {
        if ((int)value == -1)
        {
          return;
        }

        _trueid = value;
        NotifyPropertyChanged();
      }
    }

    public int HP
    {
      get => _hp;
      set
      {
        _hp = value;
        NotifyPropertyChanged();
      }
    }

    public int MaxHP
    {
      get => _maxHp;
      set
      {
        _maxHp = value;
        NotifyPropertyChanged();
      }
    }

    public int BaseHP
    {
      get => _baseHp;
      set
      {
        _baseHp = value;
        NotifyPropertyChanged();
      }
    }

    public int Attack
    {
      get => _attack;
      set
      {
        _attack = value;
        NotifyPropertyChanged();
      }
    }

    public int BaseAttack
    {
      get => _baseAttack;
      set
      {
        _baseAttack = value;
        NotifyPropertyChanged();
      }
    }

    public int Defense
    {
      get => _defense;
      set
      {
        _defense = value;
        NotifyPropertyChanged();
      }
    }

    public int BaseDefense
    {
      get => _baseDefense;
      set
      {
        _baseDefense = value;
        NotifyPropertyChanged();
      }
    }

    public override void ResetToDefault()
    {
      Trueid = 0;
      HP = 0;
      MaxHP = 0;
      BaseHP = 0;
      Attack = 0;
      BaseAttack = 0;
      Defense = 0;
      BaseDefense = 0;
    }

    public override void Parse(string str)
    {
      string[] data = str.Split(Utils.PrimarySeparator);

      Trueid = (AnimID)ParseField<int>(data[0], nameof(Trueid));
      HP = ParseField<int>(data[1], nameof(HP));
      MaxHP = ParseField<int>(data[2], nameof(MaxHP));
      BaseHP = ParseField<int>(data[3], nameof(BaseHP));
      Attack = ParseField<int>(data[4], nameof(Attack));
      BaseAttack = ParseField<int>(data[5], nameof(BaseAttack));
      Defense = ParseField<int>(data[6], nameof(Defense));
      BaseDefense = ParseField<int>(data[7], nameof(BaseDefense));
    }

    public override string ToString()
    {
      StringBuilder sb = new();

      sb.Append((int)Trueid);
      sb.Append(Utils.PrimarySeparator);
      sb.Append(HP);
      sb.Append(Utils.PrimarySeparator);
      sb.Append(MaxHP);
      sb.Append(Utils.PrimarySeparator);
      sb.Append(BaseHP);
      sb.Append(Utils.PrimarySeparator);
      sb.Append(Attack);
      sb.Append(Utils.PrimarySeparator);
      sb.Append(BaseAttack);
      sb.Append(Utils.PrimarySeparator);
      sb.Append(Defense);
      sb.Append(Utils.PrimarySeparator);
      sb.Append(BaseDefense);

      return sb.ToString();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
