using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using BugFablesSaveEditor.Utils;

namespace BugFablesSaveEditor.BugFablesSave.Sections;

public class PartyMembers : IBugFablesSaveSection
{
  public object Data { get; set; } = new ObservableCollection<PartyMemberInfo>();

  public void ParseFromSaveLine(string saveLine)
  {
    string[] membersInfo = saveLine.Split(Common.ElementSeparator);

    ObservableCollection<PartyMemberInfo> headerInfo = (ObservableCollection<PartyMemberInfo>)Data;

    for (int i = 0; i < membersInfo.Length; i++)
    {
      string[] data = membersInfo[i].Split(Common.FieldSeparator);
      if (data.Length != 8)
      {
        throw new Exception(nameof(PartyMembers) + "[" + i + "] is in an invalid format");
      }

      PartyMemberInfo newMemberInfo = new();

      int intOut = 0;
      if (!int.TryParse(data[0], out intOut))
      {
        throw new Exception(nameof(PartyMembers) + "[" + i + "]." +
                            nameof(PartyMemberInfo.Trueid) + " failed to parse");
      }

      if (intOut < 0 || intOut >= (int)AnimID.COUNT)
      {
        throw new Exception(nameof(PartyMembers) + "[" + i + "]." +
                            nameof(PartyMemberInfo.Trueid) + ": " + intOut +
                            " is not a valid Anim ID");
      }

      newMemberInfo.Trueid = (AnimID)intOut;
      if (!int.TryParse(data[1], out intOut))
      {
        throw new Exception(nameof(PartyMembers) + "[" + i + "]." +
                            nameof(PartyMemberInfo.HP) + " failed to parse");
      }

      newMemberInfo.HP = intOut;
      if (!int.TryParse(data[2], out intOut))
      {
        throw new Exception(nameof(PartyMembers) + "[" + i + "]." +
                            nameof(PartyMemberInfo.MaxHP) + " failed to parse");
      }

      newMemberInfo.MaxHP = intOut;
      if (!int.TryParse(data[3], out intOut))
      {
        throw new Exception(nameof(PartyMembers) + "[" + i + "]." +
                            nameof(PartyMemberInfo.BaseHP) + " failed to parse");
      }

      newMemberInfo.BaseHP = intOut;
      if (!int.TryParse(data[4], out intOut))
      {
        throw new Exception(nameof(PartyMembers) + "[" + i + "]." +
                            nameof(PartyMemberInfo.Attack) + " failed to parse");
      }

      newMemberInfo.Attack = intOut;
      if (!int.TryParse(data[5], out intOut))
      {
        throw new Exception(nameof(PartyMembers) + "[" + i + "]." +
                            nameof(PartyMemberInfo.BaseAttack) + " failed to parse");
      }

      newMemberInfo.BaseAttack = intOut;
      if (!int.TryParse(data[6], out intOut))
      {
        throw new Exception(nameof(PartyMembers) + "[" + i + "]." +
                            nameof(PartyMemberInfo.Defense) + " failed to parse");
      }

      newMemberInfo.Defense = intOut;
      if (!int.TryParse(data[7], out intOut))
      {
        throw new Exception(nameof(PartyMembers) + "[" + i + "]." +
                            nameof(PartyMemberInfo.BaseDefense) + " failed to parse");
      }

      newMemberInfo.BaseDefense = intOut;

      headerInfo.Add(newMemberInfo);
    }
  }

  public string EncodeToSaveLine()
  {
    ObservableCollection<PartyMemberInfo> partyMemberInfos =
      (ObservableCollection<PartyMemberInfo>)Data;
    StringBuilder sb = new();

    for (int i = 0; i < partyMemberInfos.Count; i++)
    {
      sb.Append((int)partyMemberInfos[i].Trueid);
      sb.Append(Common.FieldSeparator);
      sb.Append(partyMemberInfos[i].HP);
      sb.Append(Common.FieldSeparator);
      sb.Append(partyMemberInfos[i].MaxHP);
      sb.Append(Common.FieldSeparator);
      sb.Append(partyMemberInfos[i].BaseHP);
      sb.Append(Common.FieldSeparator);
      sb.Append(partyMemberInfos[i].Attack);
      sb.Append(Common.FieldSeparator);
      sb.Append(partyMemberInfos[i].BaseAttack);
      sb.Append(Common.FieldSeparator);
      sb.Append(partyMemberInfos[i].Defense);
      sb.Append(Common.FieldSeparator);
      sb.Append(partyMemberInfos[i].BaseDefense);

      if (i != partyMemberInfos.Count - 1)
      {
        sb.Append(Common.ElementSeparator);
      }
    }

    return sb.ToString();
  }

  public void ResetToDefault()
  {
    ObservableCollection<PartyMemberInfo> partyMemberInfos =
      (ObservableCollection<PartyMemberInfo>)Data;
    partyMemberInfos.Clear();
  }

  public class PartyMember : INotifyPropertyChanged
  {
    private AnimID _animId;

    public AnimID AnimID
    {
      get => _animId;
      set
      {
        if ((int)value == -1)
        {
          return;
        }

        _animId = value;
        NotifyPropertyChanged();
      }
    }


    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }

  public class PartyMemberInfo : INotifyPropertyChanged
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

    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
