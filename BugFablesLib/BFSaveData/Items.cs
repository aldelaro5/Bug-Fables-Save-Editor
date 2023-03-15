using static BugFablesLib.Utils;

namespace BugFablesLib.BFSaveData;

public sealed class Items : BfDataList<BfDataList<Item>>
{
  public enum ItemPossessionType
  {
    Inventory = 0,
    KeyItem,
    Stored,
    COUNT
  }

  public BfDataList<Item> Inventory { get => this[(int)ItemPossessionType.Inventory]; }
  public BfDataList<Item> Key { get => this[(int)ItemPossessionType.KeyItem]; }
  public BfDataList<Item> Stored { get => this[(int)ItemPossessionType.Stored]; }

  public Items()
  {
    NbrExpectedElements = (int)ItemPossessionType.COUNT;
    for (int i = 0; i < NbrExpectedElements; i++)
      Add(new BfDataList<Item>(CommaSeparator));
  }
}

public sealed class Item : BfData
{
  public int Id { get; set; }
  public override void Deserialize(string str) => Id = ParseValueType<int>(str, nameof(Id));
  public override string Serialize() => Id.ToString();
  public override void ResetToDefault() => Id = 0;
}
