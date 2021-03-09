using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugFablesSaveEditor.BugFablesSave
{
  public interface IBugFablesSaveSection
  {
    public object Data { get; set; }

    public void ParseFromSaveLine(string saveLine);
    public string EncodeToSaveLine();
  }
}
