using System;
using System.Text;

namespace BugFablesLib.BFSaveData.Sections;

public sealed class Global : BfData
{
  public enum SaveProgressIcon
  {
    NoIcons = 0,
    AncientMask,
    AncientTablet,
    AncientKey,
    AncientHalf,
    Elizant,
    FlameBrooch,
    WaspKingCrown,
  }

  public int BerryCount { get; set; }
  public int CurrentArea { get; set; }
  public int CurrentMap { get; set; }
  public int Exp { get; set; }
  public int MaxMp { get; set; }
  public int MaxTp { get; set; }
  public int Mp { get; set; }
  public int NbrMaxItemsInventory { get; set; }
  public int NbrMaxItemsStorage { get; set; }
  public int NeededExp { get; set; }
  public int PlayTimeHours { get; set; }
  public int PlayTimeMinutes { get; set; }
  public int PlayTimeSeconds { get; set; }
  public int Rank { get; set; }
  public SaveProgressIcon LastProgressIcon { get; set; }
  public int Tp { get; set; }

  public override string ToString()
  {
    StringBuilder sb = new();

    sb.Append(Rank);
    sb.Append(Utils.PrimarySeparator);
    sb.Append(Exp);
    sb.Append(Utils.PrimarySeparator);
    sb.Append(NeededExp);
    sb.Append(Utils.PrimarySeparator);
    sb.Append(MaxTp);
    sb.Append(Utils.PrimarySeparator);
    sb.Append(Tp);
    sb.Append(Utils.PrimarySeparator);
    sb.Append(BerryCount);
    sb.Append(Utils.PrimarySeparator);
    sb.Append(CurrentMap);
    sb.Append(Utils.PrimarySeparator);
    sb.Append(CurrentArea);
    sb.Append(Utils.PrimarySeparator);
    sb.Append(Mp);
    sb.Append(Utils.PrimarySeparator);
    sb.Append(MaxMp);
    sb.Append(Utils.PrimarySeparator);
    sb.Append(NbrMaxItemsInventory);
    sb.Append(Utils.PrimarySeparator);
    sb.Append(NbrMaxItemsStorage);
    sb.Append(Utils.PrimarySeparator);
    sb.Append(PlayTimeHours);
    sb.Append(Utils.PrimarySeparator);
    sb.Append(PlayTimeMinutes);
    sb.Append(Utils.PrimarySeparator);
    sb.Append(PlayTimeSeconds);
    sb.Append(Utils.PrimarySeparator);
    sb.Append((int)LastProgressIcon);

    return sb.ToString();
  }

  public override void Parse(string saveLine)
  {
    string[] data = saveLine.Split(new[] {Utils.PrimarySeparator}, StringSplitOptions.None);
    if (data.Length != 16)
      throw new Exception(nameof(Global) + " is in an invalid format");

    Rank = ParseField<int>(data[0], nameof(Rank));
    Exp = ParseField<int>(data[1], nameof(Exp));
    NeededExp = ParseField<int>(data[2], nameof(NeededExp));
    MaxTp = ParseField<int>(data[3], nameof(MaxTp));
    Tp = ParseField<int>(data[4], nameof(Tp));
    BerryCount = ParseField<int>(data[5], nameof(BerryCount));
    CurrentMap = ParseField<int>(data[6], nameof(CurrentMap));
    CurrentArea = ParseField<int>(data[7], nameof(CurrentArea));
    Mp = ParseField<int>(data[8], nameof(Mp));
    MaxMp = ParseField<int>(data[9], nameof(MaxMp));
    NbrMaxItemsInventory = ParseField<int>(data[10], nameof(NbrMaxItemsInventory));
    NbrMaxItemsStorage = ParseField<int>(data[11], nameof(NbrMaxItemsStorage));
    PlayTimeHours = ParseField<int>(data[12], nameof(PlayTimeHours));
    PlayTimeMinutes = ParseField<int>(data[13], nameof(PlayTimeMinutes));
    PlayTimeSeconds = ParseField<int>(data[14], nameof(PlayTimeSeconds));
    LastProgressIcon = (SaveProgressIcon)ParseField<int>(data[15], nameof(LastProgressIcon));
  }

  public override void ResetToDefault()
  {
    Rank = 0;
    Exp = 0;
    NeededExp = 0;
    MaxTp = 0;
    Tp = 0;
    BerryCount = 0;
    CurrentMap = 0;
    CurrentArea = 0;
    Mp = 0;
    MaxMp = 0;
    NbrMaxItemsInventory = 0;
    NbrMaxItemsStorage = 0;
    PlayTimeHours = 0;
    PlayTimeMinutes = 0;
    PlayTimeSeconds = 0;
    LastProgressIcon = 0;
  }
}
