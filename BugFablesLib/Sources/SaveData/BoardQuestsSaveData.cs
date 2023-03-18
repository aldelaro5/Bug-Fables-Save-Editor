using BugFablesLib.Data;

namespace BugFablesLib.SaveData;

public sealed class BoardQuestsSaveData : BfSerializableCollection<BfSerializableCollection<BfQuest>>
{
  public enum QuestState
  {
    Open = 0,
    Taken,
    Completed
  }

  public BfSerializableCollection<BfQuest> Opened { get => this[(int)QuestState.Open]; }
  public BfSerializableCollection<BfQuest> Taken { get => this[(int)QuestState.Taken]; }
  public BfSerializableCollection<BfQuest> Completed { get => this[(int)QuestState.Completed]; }

  public BoardQuestsSaveData()
  {
    NbrExpectedElements = 3;
    for (int i = 0; i < NbrExpectedElements; i++)
      Add(new BfSerializableCollection<BfQuest>(Utils.CommaSeparator));
  }
}
