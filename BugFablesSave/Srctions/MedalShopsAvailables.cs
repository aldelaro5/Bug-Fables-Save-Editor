using BugFablesSaveEditor.BugFablesEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class MedalShopsAvailables
  {
    public List<Medal>[] Data { get; set; } = new List<Medal>[(int)MedalShop.COUNT];
  }
}
