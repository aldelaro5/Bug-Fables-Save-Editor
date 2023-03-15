using static BugFablesLib.Utils;

namespace BugFablesLib.BFSaveData;

public sealed class BoardQuests : BfDataList<BfDataList<BoardQuest>>
{
  public enum QuestState
  {
    Open = 0,
    Taken,
    Completed,
    COUNT
  }

  public BfDataList<BoardQuest> Opened { get => this[(int)QuestState.Open]; }
  public BfDataList<BoardQuest> Taken { get => this[(int)QuestState.Taken]; }
  public BfDataList<BoardQuest> Completed { get => this[(int)QuestState.Completed]; }

  public BoardQuests()
  {
    NbrExpectedElements = (int)QuestState.COUNT;
    for (int i = 0; i < NbrExpectedElements; i++)
      Add(new BfDataList<BoardQuest>(CommaSeparator));
  }
}

public sealed class BoardQuest : BfData
{
  public int Id { get; set; }
  public override void Deserialize(string str) => Id = ParseValueType<int>(str, nameof(Id));
  public override string Serialize() => Id.ToString();
  public override void ResetToDefault() => Id = 0;
}
