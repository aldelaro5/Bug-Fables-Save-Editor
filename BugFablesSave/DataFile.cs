using System;
using System.Collections.Generic;
using System.Linq;
using BugFablesSaveEditor.BugFablesSave.Sections;

namespace BugFablesSaveEditor.BugFablesSave
{
  public class DataFile
  {
    public Header Header { get; set; }
    public PartyMembers PartyMembers { get; set; }
    public GlobalInfo GlobalInfo { get; set; }
    public MedalShopsPools MedalShopsPools { get; set; }
    public MedalShopsAvailables MedalShopsAvailables { get; set; }
    public Quests Quests { get; set; }
    public Items Items { get; set; }
    public Medals Medals { get; set; }
    public SamiraSongs SamiraSongs { get; set; }
    public StatBonuses StatBonuses { get; set; }
    public Library Library { get; set; }
    public Flags Flags { get; set; }
    public Flagstrings Flagstrings { get; set; }
    public Flagvars Flagvars { get; set; }
    public RegionalFlags RegionalFlags { get; set; }
    public CrystalBerries CrystalBerries { get; set; }
    public Followers Followers { get; set; }
    public EnemyEncounters EnemyEncounters { get; set; }
  }
}
