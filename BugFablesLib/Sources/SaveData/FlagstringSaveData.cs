namespace BugFablesLib.SaveData;

public sealed class FlagstringSaveData : IBfSerializable
{
  public string Str { get; set; } = string.Empty;
  public void Deserialize(string str) => Str = str;
  public string Serialize() => Str;
}
