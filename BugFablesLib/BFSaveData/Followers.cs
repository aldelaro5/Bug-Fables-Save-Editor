using static BugFablesLib.Utils;

namespace BugFablesLib.BFSaveData;

public sealed class AnimId : BfData
{
  public int Id { get; set; }
  public override void Deserialize(string str) => Id = ParseValueType<int>(str, nameof(Id));
  public override string Serialize() => Id.ToString();
  public override void ResetToDefault() => Id = 0;
}
