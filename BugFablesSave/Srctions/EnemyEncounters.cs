using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class EnemyEncounters
  {
    public class EnemyEncounterInfo
    {
      public int NbrSeen { get; set; }
      public int NbrDefeated { get; set; }
    }

    public List<EnemyEncounterInfo> Data { get; set; }
  }
}
