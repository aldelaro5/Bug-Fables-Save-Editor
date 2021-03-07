using BugFablesSaveEditor.BugFablesEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class Library
  {
    private const int nbrSlotsPerSection = 256;

    public bool[,] Data { get; set; } = new bool[nbrSlotsPerSection, (int)LibrarySection.COUNT];
  }
}
