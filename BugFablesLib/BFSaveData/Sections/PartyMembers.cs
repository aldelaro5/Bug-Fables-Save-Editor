using System;
using System.Text;

namespace BugFablesLib.BFSaveData.Sections;

public sealed class PartyMembers : BfDataList<PartyMembers.PartyMemberInfo>
{
  public PartyMembers()
  {
    ElementSeparator = Utils.SecondarySeparator;
  }

  public class PartyMemberInfo : BfData
  {
    public int Attack { get; set; }
    public int BaseAttack { get; set; }
    public int BaseDefense { get; set; }
    public int BaseHp { get; set; }
    public int Defense { get; set; }
    public int Hp { get; set; }
    public int MaxHp { get; set; }
    public int AnimId { get; set; }

    public override void ResetToDefault()
    {
      AnimId = 0;
      Hp = 0;
      MaxHp = 0;
      BaseHp = 0;
      Attack = 0;
      BaseAttack = 0;
      Defense = 0;
      BaseDefense = 0;
    }

    public override void Parse(string str)
    {
      string[] data = str.Split(new[] {Utils.PrimarySeparator}, StringSplitOptions.None);

      AnimId = ParseField<int>(data[0], nameof(AnimId));
      Hp = ParseField<int>(data[1], nameof(Hp));
      MaxHp = ParseField<int>(data[2], nameof(MaxHp));
      BaseHp = ParseField<int>(data[3], nameof(BaseHp));
      Attack = ParseField<int>(data[4], nameof(Attack));
      BaseAttack = ParseField<int>(data[5], nameof(BaseAttack));
      Defense = ParseField<int>(data[6], nameof(Defense));
      BaseDefense = ParseField<int>(data[7], nameof(BaseDefense));
    }

    public override string ToString()
    {
      StringBuilder sb = new();

      sb.Append(AnimId);
      sb.Append(Utils.PrimarySeparator);
      sb.Append(Hp);
      sb.Append(Utils.PrimarySeparator);
      sb.Append(MaxHp);
      sb.Append(Utils.PrimarySeparator);
      sb.Append(BaseHp);
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
  }
}
