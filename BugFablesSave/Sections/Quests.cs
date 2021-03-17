using BugFablesSaveEditor.BugFablesEnums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class Quests : IBugFablesSaveSection
  {
    public object Data { get; set; } = new List<Quest>[(int)QuestState.COUNT];

    public Quests()
    {
      List<Quest>[] quests = (List<Quest>[])Data;

      for (int i = 0; i < quests.Length; i++)
        quests[i] = new List<Quest>();
    }

    public void ParseFromSaveLine(string saveLine)
    {
      string[] questsData = saveLine.Split(Common.ElementSeparator);
      if (questsData.Length != (int)QuestState.COUNT)
        throw new Exception(nameof(Quests) + " is in an invalid format");

      List<Quest>[] quests = (List<Quest>[])Data;

      for (int i = 0; i < questsData.Length; i++)
      {
        if (questsData[i] == string.Empty)
          continue;

        string[] data = questsData[i].Split(Common.FieldSeparator);
        for (int j = 0; j < data.Length; j++)
        {
          int intOut = 0;
          if (!int.TryParse(data[j], out intOut))
          {
            throw new Exception(nameof(Quests) + "[" + Enum.GetNames(typeof(QuestState))[i] +
                                "][" + j + "] failed to parse");
          }
          if (intOut < 0 || intOut >= (int)Quest.COUNT)
            throw new Exception(nameof(Quests) + "[" + Enum.GetNames(typeof(QuestState))[i] +
                                "][" + j + "]: " + intOut + " is not a valid quest ID");
          quests[i].Add((Quest)intOut);
        }
      }
    }

    public string EncodeToSaveLine()
    {
      List<Quest>[] quests = (List<Quest>[])Data;
      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < quests.Length; i++)
      {
        for (int j = 0; j < quests[i].Count; j++)
        {
          sb.Append((int)quests[i][j]);

          if (j != quests[i].Count - 1)
            sb.Append(Common.FieldSeparator);
        }

        if (quests[i].Count == 0)
          sb.Append((int)Quest.NOQUEST);

        if (i != quests.Length - 1)
          sb.Append(Common.ElementSeparator);
      }

      return sb.ToString();
    }
  }
}
