namespace BugFablesLib.SaveData;

public sealed class FlagstringSaveData : IBfData
{
  public string Str { get; set; } = string.Empty;
  public void Deserialize(string str) => Str = str;
  public string Serialize() => Str;
  public void ResetToDefault() => Str = string.Empty;
}
