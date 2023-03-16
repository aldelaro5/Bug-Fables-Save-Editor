using System.ComponentModel;

namespace BugFablesSaveEditor.Enums;

public enum Area
{
  BugariaOutskirts = 0,
  AntKingdomCity,
  SnakemouthDen,
  LostSands,
  GoldenHills,
  GoldenPath,
  GoldenSettlement,
  ForsakenLands,
  FarGrasslands,
  WildSwamplands,
  DefiantRoot,
  AncientCastle,
  BeeKingdomHive,
  HoneyFactory,
  RubberPrison,
  GiantsLair,
  MetalLake,
  MetalIsland,
  TermiteCapitol,
  WaspKingdomHive,
  BanditHideout,
  StreamMountain,
  ChomperCaves,
  FishingVillage,
  UpperSnakemouth,
  COUNT
}

// This is off by 2 to make it ComboBox compatible, starts at -2
public enum MedalEquipTarget
{
  Unequipped = 0,
  Party,
  Vi,
  Kabbu,
  Leif,
  COUNT
}
