using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave.Sections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BugFablesSaveEditor.BugFablesSave
{
  public class SaveData
  {
    public Dictionary<SaveFileSection, IBugFablesSaveSection> Sections { get; set; } =
        new Dictionary<SaveFileSection, IBugFablesSaveSection>();

    public SaveData()
    {
      Sections.Add(SaveFileSection.Header, new Header());
      Sections.Add(SaveFileSection.PartyMembers, new PartyMembers());
      Sections.Add(SaveFileSection.Global, new Global());
      Sections.Add(SaveFileSection.MedalShopsPools, new MedalShopsPools());
      Sections.Add(SaveFileSection.MedalShopsAvailables, new MedalShopsAvailables());
      Sections.Add(SaveFileSection.Quests, new Quests());
      Sections.Add(SaveFileSection.Items, new Items());
      Sections.Add(SaveFileSection.Medals, new Medals());
      Sections.Add(SaveFileSection.SamiraSongs, new SamiraSongs());
      Sections.Add(SaveFileSection.StatBonuses, new StatBonuses());
      Sections.Add(SaveFileSection.Library, new Library());
      Sections.Add(SaveFileSection.Flags, new Flags());
      Sections.Add(SaveFileSection.Flagstrings, new Flagstrings());
      Sections.Add(SaveFileSection.Flagvars, new Flagvars());
      Sections.Add(SaveFileSection.RegionalFlags, new RegionalFlags());
      Sections.Add(SaveFileSection.CrystalBerries, new CrystalBerries());
      Sections.Add(SaveFileSection.Followers, new Followers());
      Sections.Add(SaveFileSection.EnemyEncounters, new EnemyEncounters());
    }

    public void LoadFromFile(string fileName)
    {
      if (!File.Exists(fileName))
        throw new Exception("The file " + fileName + " does not exist");

      string[] saveSections;
      try
      {
        saveSections = File.ReadAllLines(fileName);
      }
      catch (Exception ex)
      {
        throw new Exception("Couldn't read the save file: " + ex.Message);
      }

      for (int i = 0; i < saveSections.Length; i++)
      {
        Sections[(SaveFileSection)i].ParseFromSaveLine(saveSections[i]);
      }
    }

    public void SaveToFile(string fileName)
    {
      StringBuilder sb = new StringBuilder();
      for (SaveFileSection i = 0; i < SaveFileSection.COUNT; i++)
      {
        sb.Append(Sections[i].EncodeToSaveLine());

        if (i != SaveFileSection.COUNT - 1)
          sb.Append('\n');
      }

      try
      {
        File.WriteAllText(fileName, sb.ToString());
      }
      catch (Exception ex)
      {
        throw new Exception("Couldn't write the save file: " + ex.Message);
      }
    }
  }
}
