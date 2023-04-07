using System.Collections.Generic;

namespace BugFablesLib;

public abstract class BfSerializableNamedId : BfNamedId, IBfSerializable
{
  public void Deserialize(string str) => Id = Utils.ParseValueType<int>(str, nameof(Id));
  public string Serialize() => Id.ToString();
}
