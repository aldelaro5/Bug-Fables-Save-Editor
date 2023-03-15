using System;
using System.Text;
using static BugFablesLib.Utils;

namespace BugFablesLib.BFSaveData;

public class PartyMember : IBfData
{
  public int Attack { get; set; }
  public int BaseAttack { get; set; }
  public int BaseDefense { get; set; }
  public int BaseHp { get; set; }
  public int Defense { get; set; }
  public int Hp { get; set; }
  public int MaxHp { get; set; }
  public AnimId AnimId { get; set; } = new();

  public void Deserialize(string str)
  {
    string[] data = str.Split(new[] { CommaSeparator }, StringSplitOptions.None);

    AnimId.Deserialize(data[0]);
    Hp = ParseValueType<int>(data[1], nameof(Hp));
    MaxHp = ParseValueType<int>(data[2], nameof(MaxHp));
    BaseHp = ParseValueType<int>(data[3], nameof(BaseHp));
    Attack = ParseValueType<int>(data[4], nameof(Attack));
    BaseAttack = ParseValueType<int>(data[5], nameof(BaseAttack));
    Defense = ParseValueType<int>(data[6], nameof(Defense));
    BaseDefense = ParseValueType<int>(data[7], nameof(BaseDefense));
  }

  public string Serialize()
  {
    StringBuilder sb = new();

    sb.Append(AnimId.Serialize());
    sb.Append(CommaSeparator);
    sb.Append(Hp);
    sb.Append(CommaSeparator);
    sb.Append(MaxHp);
    sb.Append(CommaSeparator);
    sb.Append(BaseHp);
    sb.Append(CommaSeparator);
    sb.Append(Attack);
    sb.Append(CommaSeparator);
    sb.Append(BaseAttack);
    sb.Append(CommaSeparator);
    sb.Append(Defense);
    sb.Append(CommaSeparator);
    sb.Append(BaseDefense);

    return sb.ToString();
  }

  public void ResetToDefault()
  {
    AnimId.ResetToDefault();
    Hp = 0;
    MaxHp = 0;
    BaseHp = 0;
    Attack = 0;
    BaseAttack = 0;
    Defense = 0;
    BaseDefense = 0;
  }
}
