using BugFablesSaveEditor.BugFablesEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class Quests
  {
    public List<Quest>[] Data { get; set; } = new List<Quest>[(int)QuestState.COUNT];
  }
}
