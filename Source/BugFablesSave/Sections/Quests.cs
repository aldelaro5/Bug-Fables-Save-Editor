using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BugFablesSaveEditor.Enums;

namespace BugFablesSaveEditor.BugFablesSave.Sections;

public sealed class Quests : BugFablesDataList<Quests.QuestsTypeInfo>
{
  public IList<QuestInfo> Opened { get => List[(int)QuestState.Open].List; }
  public IList<QuestInfo> Taken { get => List[(int)QuestState.Taken].List; }
  public IList<QuestInfo> Completed { get => List[(int)QuestState.Completed].List; }

  public Quests()
  {
    ElementSeparator = Utils.SecondarySeparator;
    NbrExpectedElements = (int)QuestState.COUNT;
    while (List.Count < (int)QuestState.COUNT)
      List.Add(new QuestsTypeInfo());
  }

  public sealed class QuestsTypeInfo : BugFablesDataList<QuestInfo>
  {
  }

  public sealed class QuestInfo : BugFablesData, INotifyPropertyChanged
  {
    private Quest _quest;

    public Quest Quest
    {
      get => _quest;
      set
      {
        if ((int)value == -1)
        {
          return;
        }

        _quest = value;
        NotifyPropertyChanged();
      }
    }

    public override void ResetToDefault()
    {
      Quest = 0;
    }

    public override void Parse(string str)
    {
      Quest = (Quest)ParseField<int>(str, nameof(Quest));
    }

    public override string ToString()
    {
      return ((int)Quest).ToString();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
