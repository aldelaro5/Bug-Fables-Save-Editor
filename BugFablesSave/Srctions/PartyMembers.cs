using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class PartyMembers
  {
    public class PartyMemberInfo
    {
      public int Trueid { get; set; }
      public int HP { get; set; }
      public int MaxHP { get; set; }
      public int BaseHP { get; set; }
      public int Attack { get; set; }
      public int BaseAttack { get; set; }
      public int Defense { get; set; }
      public int BaseDefense { get; set; }
    }

    public List<PartyMemberInfo> Data { get; set; }
  }
}
