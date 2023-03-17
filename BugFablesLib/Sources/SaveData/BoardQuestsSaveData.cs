using BugFablesLib.Data;

namespace BugFablesLib.SaveData;

public sealed class BoardQuestsSaveData : BfSerializableDataCollection<BfSerializableDataCollection<BfQuest>>
{
  public enum QuestState
  {
    Open = 0,
    Taken,
    Completed
  }

  public BfSerializableDataCollection<BfQuest> Opened { get => this[(int)QuestState.Open]; }
  public BfSerializableDataCollection<BfQuest> Taken { get => this[(int)QuestState.Taken]; }
  public BfSerializableDataCollection<BfQuest> Completed { get => this[(int)QuestState.Completed]; }

  public BoardQuestsSaveData()
  {
    NbrExpectedElements = 3;
    for (int i = 0; i < NbrExpectedElements; i++)
      Add(new BfSerializableDataCollection<BfQuest>(Utils.CommaSeparator));
  }
}
