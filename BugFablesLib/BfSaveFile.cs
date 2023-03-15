using System;
using System.IO;
using System.Text;
using BugFablesLib.BFSaveData;
using static BugFablesLib.Utils;

namespace BugFablesLib;

public class BfSaveFile
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

  public readonly IBfData[] Sections;

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

  public BfSaveFile()
  {
    Sections = new IBfData[]
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

  public void LoadFromFile(string fileName)
  {
    if (!File.Exists(fileName))
      throw new Exception("The file " + fileName + " does not exist");

    string[] saveSections;
    try
    {
      StringBuilder data = new(File.ReadAllText(fileName));
      for (int i = 0; i < data.Length; i++)
        data[i] = (char)(data[i] ^ 543);

      saveSections = data.ToString().Split('\n');
    }
    catch (Exception ex)
    {
      string msg = $"Couldn't read the save file: {ex.Message}";
      if (ex.InnerException != null)
        msg += $": {ex.InnerException}";
      throw new Exception(msg);
    }

    for (int i = 0; i < Sections.Length; i++)
      Sections[i].Deserialize(saveSections[i]);
  }

  public void SaveToFile(string fileName)
  {
    StringBuilder sb = new();
    for (int i = 0; i < Sections.Length; i++)
    {
      sb.Append(Sections[i].Serialize());

      if (i != Sections.Length - 1)
        sb.Append('\n');
    }

    try
    {
      for (int i = 0; i < sb.Length; i++)
        sb[i] = (char)(sb[i] ^ 543);

      File.WriteAllText(fileName, sb.ToString());
    }
    catch (Exception ex)
    {
      throw new Exception("Couldn't write the save file: " + ex.Message);
    }
  }

  public void ResetToDefault()
  {
    foreach (var s in Sections)
      s.ResetToDefault();
  }
}
