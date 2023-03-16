using BugFablesLib.NamedIds;
using static BugFablesLib.Utils;

namespace BugFablesLib.SaveData;

public sealed class MedalShopsStockSaveData : BfDataList<BfDataList<BfMedal>>
{
  public enum MedalShop
  {
    Merab = 0,
    Shades,
    COUNT
  }

  public BfDataList<BfMedal> Merab { get => this[(int)MedalShop.Merab]; }
  public BfDataList<BfMedal> Shades { get => this[(int)MedalShop.Shades]; }

  public MedalShopsStockSaveData()
  {
    ElementSeparator = AtSymbolSeparator;
    NbrExpectedElements = (int)MedalShop.COUNT;
    for (int i = 0; i < NbrExpectedElements; i++)
      Add(new BfDataList<BfMedal>(CommaSeparator));
  }
}
