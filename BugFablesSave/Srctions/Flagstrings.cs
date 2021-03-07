using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class Flagstrings
  {
    private const int NbrSlots = 15;

    public string[] Data { get; set; } = new string[NbrSlots];
  }
}
