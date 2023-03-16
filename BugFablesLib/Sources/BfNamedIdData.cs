using static BugFablesLib.Utils;

namespace BugFablesLib;

public abstract class BfNamedIdData : BfNamedId, IBfData
{
  public void ResetToDefault() => Id = 0;
  public void Deserialize(string str) => Id = ParseValueType<int>(str, nameof(Id));
  public string Serialize() => Id.ToString();
}
