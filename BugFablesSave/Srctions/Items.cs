using BugFablesSaveEditor.BugFablesEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class Items
  {
    public List<Item>[] Data { get; set; } = new List<Item>[(int)ItemPossessionType.COUNT];
  }
}
