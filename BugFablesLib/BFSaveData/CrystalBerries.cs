using static BugFablesLib.Utils;

namespace BugFablesLib.BFSaveData;

public sealed class CrystalBerryFlag : IBfData
{
  public bool Obtained { get; set; }
  public void Deserialize(string str) => Obtained = ParseValueType<bool>(str, nameof(Obtained));
  public string Serialize() => Obtained.ToString();
  public void ResetToDefault() => Obtained = false;
}
