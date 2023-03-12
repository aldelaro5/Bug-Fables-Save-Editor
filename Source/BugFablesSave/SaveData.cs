using System;
using System.IO;
using System.Text;
using BugFablesSaveEditor.BugFablesSave.Sections;

namespace BugFablesSaveEditor.BugFablesSave;

public class SaveData
{
  public readonly BugFablesData[] Sections;

  public CrystalBerries CrystalBerries { get; } = new();
  public EnemyEncounters EnemyEncounters { get; } = new();
  public Flags Flags { get; } = new();
  public Flagstrings Flagstrings { get; } = new();
  public Flagvars Flagvars { get; } = new();
  public Followers Followers { get; } = new();
  public Global Global { get; } = new();
  public Header Header { get; } = new();
  public Items Items { get; } = new();
  public Library Library { get; } = new();
  public Medals Medals { get; } = new();
  public MedalShopsAvailables MedalShopsAvailables { get; } = new();
  public MedalShopsPools MedalShopsPools { get; } = new();
  public PartyMembers PartyMembers { get; } = new();
  public Quests Quests { get; } = new();
  public RegionalFlags RegionalFlags { get; } = new();
  public SamiraSongs SamiraSongs { get; } = new();
  public StatBonuses StatBonuses { get; } = new();

  public SaveData()
  {
    Sections = new BugFablesData[]
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
      Sections[i].Parse(saveSections[i]);
  }

  public void SaveToFile(string fileName)
  {
    StringBuilder sb = new();
    for (int i = 0; i < Sections.Length; i++)
    {
      sb.Append(Sections[i]);

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
