using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class CrystalBerries
  {
    private const int NbrCrystalBerries = 50;

    public bool[] Data { get; set; } = new bool[NbrCrystalBerries];
  }
}
