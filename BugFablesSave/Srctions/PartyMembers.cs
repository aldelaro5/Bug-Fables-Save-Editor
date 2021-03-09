using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class PartyMembers : IBugFablesSaveSection
  {
    public class PartyMemberInfo
    {
      public int Trueid { get; set; }
      public int HP { get; set; }
      public int MaxHP { get; set; }
      public int BaseHP { get; set; }
      public int Attack { get; set; }
      public int BaseAttack { get; set; }
      public int Defense { get; set; }
      public int BaseDefense { get; set; }
    }

    public object Data { get; set; } = new List<PartyMemberInfo>();

    public void ParseFromSaveLine(string saveLine)
    {
      string[] membersInfo = saveLine.Split(Common.ElementSeparator);

      List<PartyMemberInfo> headerInfo = (List<PartyMemberInfo>)Data;

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
        newMemberInfo.Trueid = intOut;
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
      List<PartyMemberInfo> partyMemberInfos = (List<PartyMemberInfo>)Data;
      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < partyMemberInfos.Count; i++)
      {
        sb.Append(partyMemberInfos[i].Trueid);
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
