using System.Collections.Generic;

namespace BugFablesLib.BFSaveData.Sections;

public sealed class Items : BfDataList<Items.ItemsPossesionTypeInfo>
{
  public enum ItemPossessionType
  {
    Inventory = 0,
    KeyItem,
    Stored,
    COUNT
  }

  public IList<ItemInfo> Inventory { get => List[(int)ItemPossessionType.Inventory].List; }
  public IList<ItemInfo> Key { get => List[(int)ItemPossessionType.KeyItem].List; }
  public IList<ItemInfo> Stored { get => List[(int)ItemPossessionType.Stored].List; }

  public Items()
  {
    ElementSeparator = Utils.SecondarySeparator;
    NbrExpectedElements = (int)ItemPossessionType.COUNT;
    while (List.Count < (int)ItemPossessionType.COUNT)
      List.Add(new ItemsPossesionTypeInfo());
  }

  public sealed class ItemsPossesionTypeInfo : BfDataList<ItemInfo>
  {
  }

  public sealed class ItemInfo : BfData
  {
    public int Item { get; set; }

    public override void ResetToDefault()
    {
      Item = 0;
    }

    public override void Parse(string str)
    {
      Item = ParseField<int>(str, nameof(Item));
    }

    public override string ToString()
    {
      return Item.ToString();
    }
  }
}
