using System.Collections.Generic;

namespace BugFablesLib.BFSaveData.Sections;

public sealed class Quests : BfDataList<Quests.QuestsTypeInfo>
{
  public IList<QuestInfo> Opened { get => List[(int)QuestInfo.QuestState.Open].List; }
  public IList<QuestInfo> Taken { get => List[(int)QuestInfo.QuestState.Taken].List; }
  public IList<QuestInfo> Completed { get => List[(int)QuestInfo.QuestState.Completed].List; }

  public Quests()
  {
    ElementSeparator = Utils.SecondarySeparator;
    NbrExpectedElements = (int)QuestInfo.QuestState.COUNT;
    while (List.Count < (int)QuestInfo.QuestState.COUNT)
      List.Add(new QuestsTypeInfo());
  }

  public sealed class QuestsTypeInfo : BfDataList<QuestInfo>
  {
  }

  public sealed class QuestInfo : BfData
  {
    public enum QuestState
    {
      Open = 0,
      Taken,
      Completed,
      COUNT
    }

    public int Quest { get; set; }

    public override void ResetToDefault()
    {
      Quest = 0;
    }

    public override void Parse(string str)
    {
      Quest = ParseField<int>(str, nameof(Quest));
    }

    public override string ToString()
    {
      return Quest.ToString();
    }
  }
}
