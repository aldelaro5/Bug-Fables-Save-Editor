using BugFablesSaveEditor.BugFablesEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class GlobalInfo
  {
    public int Rank { get; set; }
    public int Exp { get; set; }
    public int NeededExp { get; set; }
    public int MaxTP { get; set; }
    public int TP { get; set; }
    public int BerryCount { get; set; }
    public Map CurrentMap { get; set; }
    public Area CurrentArea { get; set; }
    public int MP { get; set; }
    public int MaxMP { get; set; }
    public int NbrMaxItemsInventory { get; set; }
    public int NbrMaxItemsStorage { get; set; }
    public int PlayTimeHours { get; set; }
    public int PlayTimeMinutes { get; set; }
    public int PlayTimeSeconds { get; set; }
    public SaveProgressIcon SaveProgressIcons { get; set; }
  }
}
