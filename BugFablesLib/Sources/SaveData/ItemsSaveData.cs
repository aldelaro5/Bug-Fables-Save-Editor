using BugFablesLib.Data;
using static BugFablesLib.Utils;

namespace BugFablesLib.SaveData;

public sealed class ItemsSaveData : BfSerializableCollection<BfSerializableCollection<BfItem>>
{
  public enum ItemPossessionType
  {
    Inventory = 0,
    KeyItem,
    Stored
  }

  public BfSerializableCollection<BfItem> Inventory { get => this[(int)ItemPossessionType.Inventory]; }
  public BfSerializableCollection<BfItem> KeyItems { get => this[(int)ItemPossessionType.KeyItem]; }
  public BfSerializableCollection<BfItem> Stored { get => this[(int)ItemPossessionType.Stored]; }

  public ItemsSaveData()
  {
    NbrExpectedElements = 3;
    for (int i = 0; i < NbrExpectedElements; i++)
      Add(new BfSerializableCollection<BfItem>(CommaSeparator));
  }
}
