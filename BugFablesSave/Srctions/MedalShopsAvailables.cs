using BugFablesSaveEditor.BugFablesEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class MedalShopsAvailables : IBugFablesSaveSection
  {
    public object Data { get; set; } = new List<Medal>[(int)MedalShop.COUNT];

    public MedalShopsAvailables()
    {
      List<Medal>[] medals = (List<Medal>[])Data;

      for (int i = 0; i < medals.Length; i++)
        medals[i] = new List<Medal>();
    }

    public void ParseFromSaveLine(string saveLine)
    {
      string[] medalsAvailable = saveLine.Split(Common.ElementSeparator);
      if (medalsAvailable.Length != (int)MedalShop.COUNT)
        throw new Exception(nameof(MedalShopsAvailables) + " is in an invalid format");

      List<Medal>[] medals = (List<Medal>[])Data;

      for (int i = 0; i < medalsAvailable.Length; i++)
      {
        if (medalsAvailable[i] == string.Empty)
          continue;

        string[] data = medalsAvailable[i].Split(Common.FieldSeparator);
        for (int j = 0; j < data.Length; j++)
        {
          int intOut = 0;
          if (!int.TryParse(data[j], out intOut))
          {
            throw new Exception(nameof(MedalShopsAvailables) + "[" + Enum.GetNames(typeof(MedalShop))[i] +
                                "][" + j + "] failed to parse");
          }
          if (intOut < 0 || intOut >= (int)Medal.COUNT)
          {
            throw new Exception(nameof(MedalShopsAvailables) + "[" + Enum.GetNames(typeof(MedalShop))[i] +
                                "][" + j + "]: " + intOut + " is not a valid medal ID");
          }
          medals[i].Add((Medal)intOut);
        }
      }
    }

    public string EncodeToSaveLine()
    {
      List<Medal>[] medals = (List<Medal>[])Data;
      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < medals.Length; i++)
      {
        for (int j = 0; j < medals[i].Count; j++)
        {
          sb.Append((int)medals[i][j]);

          if (j != medals[i].Count - 1)
            sb.Append(Common.FieldSeparator);
        }

        if (i != medals.Length - 1)
          sb.Append(Common.ElementSeparator);
      }

      return sb.ToString();
    }
  }
}
