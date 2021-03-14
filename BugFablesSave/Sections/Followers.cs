using BugFablesSaveEditor.BugFablesEnums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class Followers : IBugFablesSaveSection
  {
    public object Data { get; set; } = new ObservableCollection<AnimID>();

    public string EncodeToSaveLine()
    {
      ObservableCollection<AnimID> followers = (ObservableCollection<AnimID>)Data;
      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < followers.Count; i++)
      {
        sb.Append((int)followers[i]);

        if (i != followers.Count - 1)
          sb.Append(Common.FieldSeparator);
      }

      return sb.ToString();
    }

    public void ParseFromSaveLine(string saveLine)
    {
      string[] followersData = saveLine.Split(Common.FieldSeparator);
      ObservableCollection<AnimID> followers = (ObservableCollection<AnimID>)Data;

      for (int i = 0; i < followersData.Length; i++)
      {
        if (followersData[i] == string.Empty)
          continue;

        int intOut = 0;
        if (!int.TryParse(followersData[i], out intOut))
          throw new Exception(nameof(Followers) + "[" + i + "] failed to parse");
        if (intOut < 0 || intOut >= (int)AnimID.COUNT)
          throw new Exception(nameof(Followers) + "[" + i + "]: " + intOut + " is not a valid anim ID");
        followers[i] = (AnimID)intOut;
      }
    }
  }
}
