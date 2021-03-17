using BugFablesSaveEditor.BugFablesEnums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class MedalShopsPools : IBugFablesSaveSection
  {
    public object Data { get; set; } = new List<Medal>[(int)MedalShop.COUNT];

    public MedalShopsPools()
    {
      List<Medal>[] medals = (List<Medal>[])Data;

      for (int i = 0; i < medals.Length; i++)
        medals[i] = new List<Medal>();
    }

    public void ParseFromSaveLine(string saveLine)
    {
      string[] medalPools = saveLine.Split(Common.ElementSeparator);
      if (medalPools.Length != (int)MedalShop.COUNT)
        throw new Exception(nameof(MedalShopsPools) + " is in an invalid format");

      List<Medal>[] medals = (List<Medal>[])Data;

      for (int i = 0; i < medalPools.Length; i++)
      {
        if (medalPools[i] == string.Empty)
          continue;

        string[] data = medalPools[i].Split(Common.FieldSeparator);
        for (int j = 0; j < data.Length; j++)
        {
          int intOut = 0;
          if (!int.TryParse(data[j], out intOut))
          {
            throw new Exception(nameof(MedalShopsPools) + "[" + Enum.GetNames(typeof(MedalShop))[i] +
                                "][" + j + "] failed to parse");
          }
          if (intOut < 0 || intOut >= (int)Medal.COUNT)
          {
            throw new Exception(nameof(MedalShopsPools) + "[" + Enum.GetNames(typeof(MedalShop))[i] +
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
