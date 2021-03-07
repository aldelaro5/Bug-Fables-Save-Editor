using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class RegionalFlags
  {
    private const int NbrSlots = 100;

    public bool[] Data { get; set; } = new bool[NbrSlots];
  }
}
