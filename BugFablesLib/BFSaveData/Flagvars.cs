using static BugFablesLib.Utils;

namespace BugFablesLib.BFSaveData;

public sealed class Flagvar : BfData
{
  public int Var { get; set; }
  public override void Deserialize(string str) => Var = ParseValueType<int>(str, nameof(Var));
  public override string Serialize() => Var.ToString();
  public override void ResetToDefault() => Var = 0;
}
