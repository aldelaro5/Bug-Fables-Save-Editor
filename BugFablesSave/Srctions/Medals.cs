using BugFablesSaveEditor.BugFablesEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class Medals
  {
    public class MedalEquip
    {
      public Medal Medal { get; set; }
      public MedalEquipTarget MedalEquipTarget { get; set; }
    }

    public List<MedalEquip> Data { get; set; }
  }
}
