﻿using BugFablesLib.Data;

namespace BugFablesLib.SaveData;

public sealed class
  MedalShopsStockSaveData : BfSerializableCollection<BfSerializableCollection<BfMedal>>
{
  public enum MedalShop
  {
    Merab = 0,
    Shades
  }

  public BfSerializableCollection<BfMedal> Merab { get => this[(int)MedalShop.Merab]; }
  public BfSerializableCollection<BfMedal> Shades { get => this[(int)MedalShop.Shades]; }

  public MedalShopsStockSaveData()
  {
    ElementSeparator = Utils.AtSymbolSeparator;
    NbrExpectedElements = 2;
    for (int i = 0; i < NbrExpectedElements; i++)
      Add(new BfSerializableCollection<BfMedal>(Utils.CommaSeparator));
  }
}
