using static BugFablesLib.Utils;

namespace BugFablesLib.BFSaveData;

public sealed class CrystalBerryFlag : BfData
{
  public bool Obtained { get; set; }

  public override void Deserialize(string str) =>
    Obtained = ParseValueType<bool>(str, nameof(Obtained));

  public override string Serialize() => Obtained.ToString();
  public override void ResetToDefault() => Obtained = false;
}
