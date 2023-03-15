using static BugFablesLib.Utils;

namespace BugFablesLib.BFSaveData;

public sealed class AnimId : IBfData
{
  public int Id { get; set; }
  public void Deserialize(string str) => Id = ParseValueType<int>(str, nameof(Id));
  public string Serialize() => Id.ToString();
  public void ResetToDefault() => Id = 0;
}
