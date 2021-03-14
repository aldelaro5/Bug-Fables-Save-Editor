using BugFablesSaveEditor.BugFablesEnums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class PartyMembers : IBugFablesSaveSection
  {
    public class PartyMemberInfo : INotifyPropertyChanged
    {
      private AnimID _trueid;
      public AnimID Trueid { get { return _trueid; } set { _trueid = value; NotifyPropertyChanged(); } }

      private int _hp;
      public int HP { get { return _hp; } set { _hp = value; NotifyPropertyChanged(); } }

      private int _maxHp;
      public int MaxHP { get { return _maxHp; } set { _maxHp = value; NotifyPropertyChanged(); } }

      private int _baseHp;
      public int BaseHP { get { return _baseHp; } set { _baseHp = value; NotifyPropertyChanged(); } }

      private int _attack;
      public int Attack { get { return _attack; } set { _attack = value; NotifyPropertyChanged(); } }

      private int _baseAttack;
      public int BaseAttack { get { return _baseAttack; } set { _baseAttack = value; NotifyPropertyChanged(); } }

      private int _defense;
      public int Defense { get { return _defense; } set { _defense = value; NotifyPropertyChanged(); } }

      private int _baseDefense;
      public int BaseDefense { get { return _baseDefense; } set { _baseDefense = value; NotifyPropertyChanged(); } }

      public event PropertyChangedEventHandler? PropertyChanged;
      private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
      {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    public object Data { get; set; } = new ObservableCollection<PartyMemberInfo>();

    public void ParseFromSaveLine(string saveLine)
    {
      string[] membersInfo = saveLine.Split(Common.ElementSeparator);

      ObservableCollection<PartyMemberInfo> headerInfo = (ObservableCollection<PartyMemberInfo>)Data;

      for (int i = 0; i < membersInfo.Length; i++)
      {
        string[] data = membersInfo[i].Split(Common.FieldSeparator);
        if (data.Length != 8)
          throw new Exception(nameof(PartyMembers) + "[" + i + "] is in an invalid format");

        PartyMemberInfo newMemberInfo = new PartyMemberInfo();

        int intOut = 0;
        if (!int.TryParse(data[0], out intOut))
        {
          throw new Exception(nameof(PartyMembers) + "[" + i + "]." +
                              nameof(PartyMemberInfo.Trueid) + " failed to parse");
        }
        if (intOut < 0 || intOut >= (int)AnimID.COUNT)
        {
          throw new Exception(nameof(PartyMembers) + "[" + i + "]." +
                              nameof(PartyMemberInfo.Trueid) + ": " + intOut + " is not a valid Anim ID");
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
      ObservableCollection<PartyMemberInfo> partyMemberInfos = (ObservableCollection<PartyMemberInfo>)Data;
      StringBuilder sb = new StringBuilder();

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
          sb.Append(Common.ElementSeparator);
      }

      return sb.ToString();
    }
  }
}
