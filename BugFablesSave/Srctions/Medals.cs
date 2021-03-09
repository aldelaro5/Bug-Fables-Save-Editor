using BugFablesSaveEditor.BugFablesEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class Medals : IBugFablesSaveSection
  {
    public class MedalEquip
    {
      public Medal Medal { get; set; }
      public MedalEquipTarget MedalEquipTarget { get; set; }
    }

    public object Data { get; set; } = new List<MedalEquip>();

    public void ParseFromSaveLine(string saveLine)
    {
      string[] medalsData = saveLine.Split(Common.ElementSeparator);
      List<MedalEquip> medals = (List<MedalEquip>)Data;

      for (int i = 0; i < medalsData.Length; i++)
      {
        if (medalsData[i] == string.Empty)
          continue;

        string[] data = medalsData[i].Split(Common.FieldSeparator);

        MedalEquip newMedalEquip = new MedalEquip();

        int intOut = 0;
        if (!int.TryParse(data[0], out intOut))
          throw new Exception(nameof(Medals) + "[" + i + "]." + nameof(MedalEquip.Medal) + " failed to parse");
        if (intOut < 0 || intOut >= (int)Medal.COUNT)
          throw new Exception(nameof(Medals) + "[" + i + "]." + nameof(MedalEquip.Medal) + ": " + intOut + " is not a valid medal ID");
        newMedalEquip.Medal = (Medal)intOut;
        if (!int.TryParse(data[1], out intOut))
          throw new Exception(nameof(Medals) + "[" + i + "]." + nameof(MedalEquip.MedalEquipTarget) + " failed to parse");
        if (intOut < -2 || intOut >= (int)MedalEquipTarget.COUNT)
          throw new Exception(nameof(Medals) + "[" + i + "]." + nameof(MedalEquip.MedalEquipTarget) + ": " + intOut + " is not a valid medal equip target value");
        newMedalEquip.MedalEquipTarget = (MedalEquipTarget)intOut;

        medals.Add(newMedalEquip);
      }
    }

    public string EncodeToSaveLine()
    {
      List<MedalEquip> medals = (List<MedalEquip>)Data;
      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < medals.Count; i++)
      {
        sb.Append((int)medals[i].Medal);
        sb.Append(Common.FieldSeparator);
        sb.Append((int)medals[i].MedalEquipTarget);

        if (i != medals.Count - 1)
          sb.Append(Common.ElementSeparator);
      }

      return sb.ToString();
    }
  }
}
