using System;
using System.Text;
using BugFablesLib.Data;

namespace BugFablesLib.SaveData;

public sealed class GlobalSaveData : IBfSerializable
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
  public BfArea CurrentArea { get; set; } = new();
  public BfMap CurrentMap { get; set; } = new();
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

  public string Serialize()
  {
    StringBuilder sb = new();

    sb.Append(Rank);
    sb.Append(Utils.CommaSeparator);
    sb.Append(Exp);
    sb.Append(Utils.CommaSeparator);
    sb.Append(NeededExp);
    sb.Append(Utils.CommaSeparator);
    sb.Append(MaxTp);
    sb.Append(Utils.CommaSeparator);
    sb.Append(Tp);
    sb.Append(Utils.CommaSeparator);
    sb.Append(BerryCount);
    sb.Append(Utils.CommaSeparator);
    sb.Append(CurrentMap.Id);
    sb.Append(Utils.CommaSeparator);
    sb.Append(CurrentArea.Id);
    sb.Append(Utils.CommaSeparator);
    sb.Append(Mp);
    sb.Append(Utils.CommaSeparator);
    sb.Append(MaxMp);
    sb.Append(Utils.CommaSeparator);
    sb.Append(NbrMaxItemsInventory);
    sb.Append(Utils.CommaSeparator);
    sb.Append(NbrMaxItemsStorage);
    sb.Append(Utils.CommaSeparator);
    sb.Append(PlayTimeHours);
    sb.Append(Utils.CommaSeparator);
    sb.Append(PlayTimeMinutes);
    sb.Append(Utils.CommaSeparator);
    sb.Append(PlayTimeSeconds);
    sb.Append(Utils.CommaSeparator);
    sb.Append((int)LastProgressIcon);

    return sb.ToString();
  }

  public void Deserialize(string saveLine)
  {
    string[] data = saveLine.Split(new[] { Utils.CommaSeparator }, StringSplitOptions.None);
    if (data.Length != 16)
      throw new Exception(nameof(GlobalSaveData) + " is in an invalid format");

    Rank = Utils.ParseValueType<int>(data[0], nameof(Rank));
    Exp = Utils.ParseValueType<int>(data[1], nameof(Exp));
    NeededExp = Utils.ParseValueType<int>(data[2], nameof(NeededExp));
    MaxTp = Utils.ParseValueType<int>(data[3], nameof(MaxTp));
    Tp = Utils.ParseValueType<int>(data[4], nameof(Tp));
    BerryCount = Utils.ParseValueType<int>(data[5], nameof(BerryCount));
    CurrentMap.Id = Utils.ParseValueType<int>(data[6], nameof(CurrentMap));
    CurrentArea.Id = Utils.ParseValueType<int>(data[7], nameof(CurrentArea));
    Mp = Utils.ParseValueType<int>(data[8], nameof(Mp));
    MaxMp = Utils.ParseValueType<int>(data[9], nameof(MaxMp));
    NbrMaxItemsInventory = Utils.ParseValueType<int>(data[10], nameof(NbrMaxItemsInventory));
    NbrMaxItemsStorage = Utils.ParseValueType<int>(data[11], nameof(NbrMaxItemsStorage));
    PlayTimeHours = Utils.ParseValueType<int>(data[12], nameof(PlayTimeHours));
    PlayTimeMinutes = Utils.ParseValueType<int>(data[13], nameof(PlayTimeMinutes));
    PlayTimeSeconds = Utils.ParseValueType<int>(data[14], nameof(PlayTimeSeconds));
    LastProgressIcon =
      (SaveProgressIcon)Utils.ParseValueType<int>(data[15], nameof(LastProgressIcon));
  }

  public void ResetToDefault()
  {
    Rank = 0;
    Exp = 0;
    NeededExp = 0;
    MaxTp = 0;
    Tp = 0;
    BerryCount = 0;
    CurrentMap.Id = 0;
    CurrentArea.Id = 0;
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
