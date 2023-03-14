using System.Collections.Generic;

namespace BugFablesLib.BFSaveData.Sections;

public sealed class
  MedalShopsPools : BfDataList<MedalShopsPools.MedalsShopPoolInfo>
{
  public enum MedalShop
  {
    Merab = 0,
    Shades,
    COUNT
  }

  public IList<MedalInShopPoolInfo> Merab { get => List[(int)MedalShop.Merab].List; }
  public IList<MedalInShopPoolInfo> Shades { get => List[(int)MedalShop.Shades].List; }

  public MedalShopsPools()
  {
    ElementSeparator = Utils.SecondarySeparator;
    NbrExpectedElements = (int)MedalShop.COUNT;
    while (List.Count < (int)MedalShop.COUNT)
      List.Add(new MedalsShopPoolInfo());
  }

  public sealed class MedalsShopPoolInfo : BfDataList<MedalInShopPoolInfo>
  {
  }

  public sealed class MedalInShopPoolInfo : BfData
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
