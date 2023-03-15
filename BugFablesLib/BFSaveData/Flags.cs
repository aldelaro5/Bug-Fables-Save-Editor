using static BugFablesLib.Utils;

namespace BugFablesLib.BFSaveData;

public sealed class Flag : BfData
{
  public bool Enabled { get; set; }
  public override void Deserialize(string str) => Enabled = ParseValueType<bool>(str, nameof(Enabled));
  public override string Serialize() => Enabled.ToString();
  public override void ResetToDefault() => Enabled = false;
}
