using static BugFablesLib.Utils;

namespace BugFablesLib.SaveData;

public sealed class CrystalBerryFlagSaveData : IBfSerializable
{
  public bool Obtained { get; set; }

  public void Deserialize(string str) =>
    Obtained = ParseValueType<bool>(str, nameof(Obtained));

  public string Serialize() => Obtained.ToString();
  public void ResetToDefault() => Obtained = false;
}
