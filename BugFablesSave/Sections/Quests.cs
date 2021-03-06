using BugFablesSaveEditor.BugFablesEnums;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class Quests : IBugFablesSaveSection
  {
    public class QuestInfo : INotifyPropertyChanged
    {
      private Quest _quest;
      public Quest Quest
      {
        get { return _quest; }
        set
        {
          if ((int)value == -1)
            return;

          _quest = value;
          NotifyPropertyChanged();
        }
      }

      public event PropertyChangedEventHandler? PropertyChanged;
      private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
      {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    public object Data { get; set; } = new ObservableCollection<QuestInfo>[(int)QuestState.COUNT];

    public Quests()
    {
      ObservableCollection<QuestInfo>[] quests = (ObservableCollection<QuestInfo>[])Data;

      for (int i = 0; i < quests.Length; i++)
        quests[i] = new ObservableCollection<QuestInfo>();
    }

    public void ParseFromSaveLine(string saveLine)
    {
      string[] questsData = saveLine.Split(CommonUtils.ElementSeparator);
      if (questsData.Length != (int)QuestState.COUNT)
        throw new Exception(nameof(Quests) + " is in an invalid format");

      ObservableCollection<QuestInfo>[] quests = (ObservableCollection<QuestInfo>[])Data;

      for (int i = 0; i < questsData.Length; i++)
      {
        if (questsData[i] == string.Empty)
          continue;

        string[] data = questsData[i].Split(CommonUtils.FieldSeparator);
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
          quests[i].Add(new QuestInfo { Quest = (Quest)intOut });
        }
      }
    }

    public string EncodeToSaveLine()
    {
      ObservableCollection<QuestInfo>[] quests = (ObservableCollection<QuestInfo>[])Data;
      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < quests.Length; i++)
      {
        for (int j = 0; j < quests[i].Count; j++)
        {
          sb.Append((int)quests[i][j].Quest);

          if (j != quests[i].Count - 1)
            sb.Append(CommonUtils.FieldSeparator);
        }

        if (quests[i].Count == 0)
          sb.Append((int)Quest.NOQUEST);

        if (i != quests.Length - 1)
          sb.Append(CommonUtils.ElementSeparator);
      }

      return sb.ToString();
    }

    public void ResetToDefault()
    {
      ObservableCollection<QuestInfo>[] quests = (ObservableCollection<QuestInfo>[])Data;
      foreach (var collection in quests)
        collection.Clear();
    }
  }
}
