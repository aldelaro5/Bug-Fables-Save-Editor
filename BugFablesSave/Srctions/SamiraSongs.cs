using BugFablesSaveEditor.BugFablesEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class SamiraSongs
  {
    private const int songNotBought = -1;
    private const int songBought = -1;

    public class AvailableSong
    {
      public Song Song { get; set; }
      public bool IsBought { get; set; }
    }

    public List<AvailableSong> Data { get; set; }
  }
}
