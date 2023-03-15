using static BugFablesLib.Utils;

namespace BugFablesLib.BFSaveData;

public sealed class MedalShopsStock : BfDataList<BfDataList<Medal>>
{
  public enum MedalShop
  {
    Merab = 0,
    Shades,
    COUNT
  }

  public BfDataList<Medal> Merab { get => this[(int)MedalShop.Merab]; }
  public BfDataList<Medal> Shades { get => this[(int)MedalShop.Shades]; }

  public MedalShopsStock()
  {
    ElementSeparator = AtSymbolSeparator;
    NbrExpectedElements = (int)MedalShop.COUNT;
    for (int i = 0; i < NbrExpectedElements; i++)
      Add(new BfDataList<Medal>(CommaSeparator));
  }
}

public sealed class Medal : BfData
{
  public int Id { get; set; }
  public override void Deserialize(string str) => Id = ParseValueType<int>(str, nameof(Id));
  public override string Serialize() => Id.ToString();
  public override void ResetToDefault() => Id = 0;
}
