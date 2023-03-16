using static BugFablesLib.Utils;

namespace BugFablesLib.SaveData;

public sealed class FlagSaveData : IBfData
{
  public bool Enabled { get; set; }

  public void Deserialize(string str) =>
    Enabled = ParseValueType<bool>(str, nameof(Enabled));

  public string Serialize() => Enabled.ToString();
  public void ResetToDefault() => Enabled = false;
}
