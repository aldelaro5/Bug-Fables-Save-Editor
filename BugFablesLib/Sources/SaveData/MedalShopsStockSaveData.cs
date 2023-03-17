using BugFablesLib.Data;
using static BugFablesLib.Utils;

namespace BugFablesLib.SaveData;

public sealed class MedalShopsStockSaveData : BfSerializableDataCollection<BfSerializableDataCollection<BfMedal>>
{
  public enum MedalShop
  {
    Merab = 0,
    Shades
  }

  public BfSerializableDataCollection<BfMedal> Merab { get => this[(int)MedalShop.Merab]; }
  public BfSerializableDataCollection<BfMedal> Shades { get => this[(int)MedalShop.Shades]; }

  public MedalShopsStockSaveData()
  {
    ElementSeparator = AtSymbolSeparator;
    NbrExpectedElements = 2;
    for (int i = 0; i < NbrExpectedElements; i++)
      Add(new BfSerializableDataCollection<BfMedal>(CommaSeparator));
  }
}
