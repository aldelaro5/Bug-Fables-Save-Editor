using BugFablesSaveEditor.BugFablesEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class StatBonuses
  {
    public class StatBonusInfo
    {
      public StatBonusType Type { get; set; }
      public int Amount { get; set; }
      public StatBonusTarget Target { get; set; }
    }

    public List<StatBonusInfo> Data { get; set; }
  }
}
