using System.Collections.Generic;

namespace BugFablesLib.BFSaveData.Sections;

public sealed class
  MedalShopsAvailables : BfDataList<MedalShopsAvailables.MedalsShopAvailableInfo>
{
  public enum MedalShop
  {
    Merab = 0,
    Shades,
    COUNT
  }

  public IList<MedalInShopAvailableInfo> Merab { get => List[(int)MedalShop.Merab].List; }
  public IList<MedalInShopAvailableInfo> Shades { get => List[(int)MedalShop.Shades].List; }

  public MedalShopsAvailables()
  {
    ElementSeparator = Utils.SecondarySeparator;
    NbrExpectedElements = (int)MedalShop.COUNT;
    while (List.Count < (int)MedalShop.COUNT)
      List.Add(new MedalsShopAvailableInfo());
  }

  public sealed class MedalsShopAvailableInfo : BfDataList<MedalInShopAvailableInfo>
  {
  }

  public sealed class MedalInShopAvailableInfo : BfData
  {
    public int Medal { get; set; }

    public override void ResetToDefault()
    {
      Medal = 0;
    }

    public override void Parse(string str)
    {
      Medal = ParseField<int>(str, nameof(Medal));
    }

    public override string ToString()
    {
      return Medal.ToString();
    }
  }
}
