using BugFablesLib.NamedIds;

namespace BugFablesLib.SaveData;

public sealed class BoardQuestsSaveData : BfDataList<BfDataList<BfQuest>>
{
  public enum QuestState
  {
    Open = 0,
    Taken,
    Completed,
    COUNT
  }

  public BfDataList<BfQuest> Opened { get => this[(int)QuestState.Open]; }
  public BfDataList<BfQuest> Taken { get => this[(int)QuestState.Taken]; }
  public BfDataList<BfQuest> Completed { get => this[(int)QuestState.Completed]; }

  public BoardQuestsSaveData()
  {
    NbrExpectedElements = (int)QuestState.COUNT;
    for (int i = 0; i < NbrExpectedElements; i++)
      Add(new BfDataList<BfQuest>(Utils.CommaSeparator));
  }
}
