namespace BugFablesLib.BFSaveData;

public sealed class Flagstring : IBfData
{
  public string Str { get; set; } = "";
  public void Deserialize(string str) => Str = str;
  public string Serialize() => Str;
  public void ResetToDefault() => Str = string.Empty;
}
