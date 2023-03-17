using BugFablesLib.Data;
using static BugFablesLib.Utils;

namespace BugFablesLib.SaveData;

public sealed class ItemsSaveData : BfSerializableDataCollection<BfSerializableDataCollection<BfItem>>
{
  public enum ItemPossessionType
  {
    Inventory = 0,
    KeyItem,
    Stored
  }

  public BfSerializableDataCollection<BfItem> Inventory { get => this[(int)ItemPossessionType.Inventory]; }
  public BfSerializableDataCollection<BfItem> Key { get => this[(int)ItemPossessionType.KeyItem]; }
  public BfSerializableDataCollection<BfItem> Stored { get => this[(int)ItemPossessionType.Stored]; }

  public ItemsSaveData()
  {
    NbrExpectedElements = 3;
    for (int i = 0; i < NbrExpectedElements; i++)
      Add(new BfSerializableDataCollection<BfItem>(CommaSeparator));
  }
}
