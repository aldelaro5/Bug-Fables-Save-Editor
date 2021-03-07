using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class Flagvars
  {
    private const int NbrSlots = 70;

    public int[] Data { get; set; } = new int[NbrSlots];
  }
}
