using static BugFablesLib.Utils;

namespace BugFablesLib.BFSaveData;

public sealed class MedalShopsStock : BfList<BfList<Medal>>
{
  public enum MedalShop
  {
    Merab = 0,
    Shades,
    COUNT
  }

  public BfList<Medal> Merab { get => this[(int)MedalShop.Merab]; }
  public BfList<Medal> Shades { get => this[(int)MedalShop.Shades]; }

  public MedalShopsStock()
  {
    ElementSeparator = AtSymbolSeparator;
    NbrExpectedElements = (int)MedalShop.COUNT;
    for (int i = 0; i < NbrExpectedElements; i++)
      Add(new BfList<Medal>(CommaSeparator));
  }
}

public sealed class Medal : IBfData
{
  public int Id { get; set; }
  public void Deserialize(string str) => Id = ParseValueType<int>(str, nameof(Id));
  public string Serialize() => Id.ToString();
  public void ResetToDefault() => Id = 0;
}
