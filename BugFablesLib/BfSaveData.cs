using System.Collections.Generic;
using System.Text;
using BugFablesLib.BFSaveData;
using static BugFablesLib.Utils;

namespace BugFablesLib;

public abstract class BfSaveData : IBfDataContainer
{
  public enum SaveFileSection
  {
    Header = 0,
    PartyMembers,
    Global,
    MedalShopsAvailables,
    MedalShopsPools,
    Quests,
    Items,
    Medals,
    SamiraSongs,
    StatBonuses,
    Library,
    Flags,
    Flagstrings,
    Flagvars,
    RegionalFlags,
    CrystalBerries,
    Followers,
    EnemyEncounters,
    COUNT
  }

  private const string FlagstringsSeparator = "|SPLIT|";

  public IList<IBfData> Data { get; }

  public BfDataList<CrystalBerryFlag> CrystalBerries { get; } = new(CommaSeparator);
  public BfDataList<EnemyEncounter> EnemyEncounters { get; } = new(AtSymbolSeparator);
  public BfDataList<Flag> Flags { get; } = new(CommaSeparator);
  public BfDataList<Flagstring> Flagstrings { get; } = new(FlagstringsSeparator);
  public BfDataList<Flagvar> Flagvars { get; } = new(CommaSeparator);
  public BfDataList<AnimId> Followers { get; } = new(CommaSeparator);
  public Global Global { get; } = new();
  public Header Header { get; } = new();
  public Items Items { get; } = new();
  public LibrarySections Library { get; } = new();
  public BfDataList<MedalOnHand> Medals { get; } = new(AtSymbolSeparator);
  public MedalShopsStock MedalShopsAvailables { get; } = new();
  public MedalShopsStock MedalShopsPools { get; } = new();
  public BfDataList<PartyMember> PartyMembers { get; } = new(AtSymbolSeparator);
  public BoardQuests Quests { get; } = new();
  public BfDataList<Flag> RegionalFlags { get; } = new(CommaSeparator);
  public BfDataList<Music> SamiraSongs { get; } = new(AtSymbolSeparator);
  public BfDataList<StatBonus> StatBonuses { get; } = new(AtSymbolSeparator);

  public BfSaveData()
  {
    Data = new List<IBfData>
    {
      Header,
      PartyMembers,
      Global,
      MedalShopsAvailables,
      MedalShopsPools,
      Quests,
      Items,
      Medals,
      SamiraSongs,
      StatBonuses,
      Library,
      Flags,
      Flagstrings,
      Flagvars,
      RegionalFlags,
      CrystalBerries,
      Followers,
      EnemyEncounters
    };
  }

  public virtual void LoadFromBytes(byte[] data)
  {
    string strData = Encoding.UTF8.GetString(data);
    string[] saveSections = strData.Split('\n');
    for (int i = 0; i < Data.Count; i++)
      Data[i].Deserialize(saveSections[i]);
  }

  public virtual byte[] EncodeToBytes()
  {
    StringBuilder sb = new();
    for (int i = 0; i < Data.Count; i++)
    {
      sb.Append(Data[i].Serialize());

      if (i != Data.Count - 1)
        sb.Append('\n');
    }

    return Encoding.UTF8.GetBytes(sb.ToString());
  }

  public void ResetToDefault()
  {
    foreach (var s in Data)
      s.ResetToDefault();
  }
}
