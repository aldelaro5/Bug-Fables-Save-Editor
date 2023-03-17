using BugFablesLib.Data;

namespace BugFablesLib.SaveData;

public sealed class BoardQuestsSaveData : BfDataCollection<BfDataCollection<BfQuest>>
{
  public enum QuestState
  {
    Open = 0,
    Taken,
    Completed
  }

  public BfDataCollection<BfQuest> Opened { get => this[(int)QuestState.Open]; }
  public BfDataCollection<BfQuest> Taken { get => this[(int)QuestState.Taken]; }
  public BfDataCollection<BfQuest> Completed { get => this[(int)QuestState.Completed]; }

  public BoardQuestsSaveData()
  {
    NbrExpectedElements = 3;
    for (int i = 0; i < NbrExpectedElements; i++)
      Add(new BfDataCollection<BfQuest>(Utils.CommaSeparator));
  }
}
