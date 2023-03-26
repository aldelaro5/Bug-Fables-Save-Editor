namespace BugFablesLib.SaveData;

public sealed class FlagSaveData : IBfSerializable
{
  public bool Enabled { get; set; }

  public void Deserialize(string str) =>
    Enabled = Utils.ParseValueType<bool>(str, nameof(Enabled));

  public string Serialize() => Enabled.ToString();
  public void ResetToDefault() => Enabled = false;
}
