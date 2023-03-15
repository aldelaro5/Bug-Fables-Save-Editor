using static BugFablesLib.Utils;

namespace BugFablesLib.BFSaveData;

public sealed class Items : BfList<BfList<Item>>
{
  public enum ItemPossessionType
  {
    Inventory = 0,
    KeyItem,
    Stored,
    COUNT
  }

  public BfList<Item> Inventory { get => this[(int)ItemPossessionType.Inventory]; }
  public BfList<Item> Key { get => this[(int)ItemPossessionType.KeyItem]; }
  public BfList<Item> Stored { get => this[(int)ItemPossessionType.Stored]; }

  public Items()
  {
    NbrExpectedElements = (int)ItemPossessionType.COUNT;
    for (int i = 0; i < NbrExpectedElements; i++)
      Add(new BfList<Item>(CommaSeparator));
  }
}

public sealed class Item : IBfData
{
  public int Id { get; set; }
  public void Deserialize(string str) => Id = ParseValueType<int>(str, nameof(Id));
  public string Serialize() => Id.ToString();
  public void ResetToDefault() => Id = 0;
}
