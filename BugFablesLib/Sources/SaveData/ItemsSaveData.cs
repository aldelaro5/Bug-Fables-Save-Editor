using BugFablesLib.Data;
using static BugFablesLib.Utils;

namespace BugFablesLib.SaveData;

public sealed class ItemsSaveData : BfDataCollection<BfDataCollection<BfItem>>
{
  public enum ItemPossessionType
  {
    Inventory = 0,
    KeyItem,
    Stored
  }

  public BfDataCollection<BfItem> Inventory { get => this[(int)ItemPossessionType.Inventory]; }
  public BfDataCollection<BfItem> Key { get => this[(int)ItemPossessionType.KeyItem]; }
  public BfDataCollection<BfItem> Stored { get => this[(int)ItemPossessionType.Stored]; }

  public ItemsSaveData()
  {
    NbrExpectedElements = 3;
    for (int i = 0; i < NbrExpectedElements; i++)
      Add(new BfDataCollection<BfItem>(CommaSeparator));
  }
}
