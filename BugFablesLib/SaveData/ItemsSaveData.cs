using BugFablesLib.NamedIds;
using static BugFablesLib.Utils;

namespace BugFablesLib.SaveData;

public sealed class ItemsSaveData : BfDataList<BfDataList<BfItem>>
{
  public enum ItemPossessionType
  {
    Inventory = 0,
    KeyItem,
    Stored,
    COUNT
  }

  public BfDataList<BfItem> Inventory { get => this[(int)ItemPossessionType.Inventory]; }
  public BfDataList<BfItem> Key { get => this[(int)ItemPossessionType.KeyItem]; }
  public BfDataList<BfItem> Stored { get => this[(int)ItemPossessionType.Stored]; }

  public ItemsSaveData()
  {
    NbrExpectedElements = (int)ItemPossessionType.COUNT;
    for (int i = 0; i < NbrExpectedElements; i++)
      Add(new BfDataList<BfItem>(CommaSeparator));
  }
}
