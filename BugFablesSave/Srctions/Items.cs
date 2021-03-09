using BugFablesSaveEditor.BugFablesEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class Items : IBugFablesSaveSection
  {
    public object Data { get; set; } = new List<Item>[(int)ItemPossessionType.COUNT];

    public Items()
    {
      List<Item>[] items = (List<Item>[])Data;

      for (int i = 0; i < items.Length; i++)
        items[i] = new List<Item>();
    }

    public void ParseFromSaveLine(string saveLine)
    {
      string[] itemsData = saveLine.Split(Common.ElementSeparator);
      if (itemsData.Length != (int)ItemPossessionType.COUNT)
        throw new Exception(nameof(Items) + " is in an invalid format");

      List<Item>[] items = (List<Item>[])Data;

      for (int i = 0; i < itemsData.Length; i++)
      {
        if (itemsData[i] == string.Empty)
          continue;

        string[] data = itemsData[i].Split(Common.FieldSeparator);
        for (int j = 0; j < data.Length; j++)
        {
          int intOut = 0;
          if (!int.TryParse(data[j], out intOut))
          {
            throw new Exception(nameof(Items) + "[" + Enum.GetNames(typeof(ItemPossessionType))[i] +
                                "][" + j + "] failed to parse");
          }
          if (intOut < 0 || intOut >= (int)Item.COUNT)
            throw new Exception(nameof(Items) + "[" + Enum.GetNames(typeof(ItemPossessionType))[i] +
                                "][" + j + "]: " + intOut + " is not a valid item ID");
          items[i].Add((Item)intOut);
        }
      }
    }

    public string EncodeToSaveLine()
    {
      List<Item>[] items = (List<Item>[])Data;
      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < items.Length; i++)
      {
        for (int j = 0; j < items[i].Count; j++)
        {
          sb.Append((int)items[i][j]);

          if (j != items[i].Count - 1)
            sb.Append(Common.FieldSeparator);
        }

        if (i != items.Length - 1)
          sb.Append(Common.ElementSeparator);
      }

      return sb.ToString();
    }
  }
}
