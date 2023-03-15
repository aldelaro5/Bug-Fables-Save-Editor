namespace BugFablesLib.BFSaveData;

public sealed class Flagstring : BfData
{
  public string Str { get; set; } = string.Empty;
  public override void Deserialize(string str) => Str = str;
  public override string Serialize() => Str;
  public override void ResetToDefault() => Str = string.Empty;
}
