using static BugFablesLib.Utils;

namespace BugFablesLib.BFSaveData;

public sealed class BoardQuests : BfList<BfList<BoardQuest>>
{
  public enum QuestState
  {
    Open = 0,
    Taken,
    Completed,
    COUNT
  }

  public BfList<BoardQuest> Opened { get => this[(int)QuestState.Open]; }
  public BfList<BoardQuest> Taken { get => this[(int)QuestState.Taken]; }
  public BfList<BoardQuest> Completed { get => this[(int)QuestState.Completed]; }

  public BoardQuests()
  {
    NbrExpectedElements = (int)QuestState.COUNT;
    for (int i = 0; i < NbrExpectedElements; i++)
      Add(new BfList<BoardQuest>(CommaSeparator));
  }
}

public sealed class BoardQuest : IBfData
{
  public int Id { get; set; }
  public void Deserialize(string str) => Id = ParseValueType<int>(str, nameof(Id));
  public string Serialize() => Id.ToString();
  public void ResetToDefault() => Id = 0;
}
