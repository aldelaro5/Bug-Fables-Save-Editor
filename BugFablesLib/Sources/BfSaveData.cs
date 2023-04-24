using System.Collections.Generic;
using System.Text;
using BugFablesLib.Data;
using BugFablesLib.SaveData;

namespace BugFablesLib;

public class BfSaveData : IBfDataContainer
{
  public static BfPcSaveDataFormat PcSaveDataFormat = new();
  public static BfSwitchSaveDataFormat SwitchSaveDataFormat = new();
  public static BfXboxPcSaveDataFormat XboxSaveDataFormat = new();

  public enum SaveFileSection
  {
    Header = 0,
    PartyMembers,
    Global,
    MedalShopsAvailables,
    MedalShopsPools,
    Quests,
    Items,
    Medals,
    SamiraSongs,
    StatBonuses,
    Library,
    Flags,
    Flagstrings,
    Flagvars,
    RegionalFlags,
    CrystalBerries,
    Followers,
    EnemyEncounters,
    COUNT
  }

  private const string FlagstringsSeparator = "|SPLIT|";

  public IList<IBfSerializable> Data { get; }

  public BfSerializableCollection<FlagSaveData> CrystalBerries { get; } = new(Utils.CommaSeparator);

  public BfSerializableCollection<EnemyEncounterSaveData> EnemyEncounters { get; } =
    new(Utils.AtSymbolSeparator);

  public BfSerializableCollection<FlagSaveData> Flags { get; } = new(Utils.CommaSeparator);

  public BfSerializableCollection<FlagstringSaveData> Flagstrings { get; } =
    new(FlagstringsSeparator);

  public BfSerializableCollection<FlagvarSaveData> Flagvars { get; } = new(Utils.CommaSeparator);
  public BfSerializableCollection<BfAnimId> Followers { get; } = new(Utils.CommaSeparator);
  public GlobalSaveData Global { get; } = new();
  public HeaderSaveData Header { get; } = new();
  public ItemsSaveData Items { get; } = new();
  public LibrarySaveData Library { get; } = new();

  public BfSerializableCollection<BfMedalOnHandSaveData> Medals { get; } =
    new(Utils.AtSymbolSeparator);

  public MedalShopsStockSaveData MedalShopsAvailables { get; } = new();
  public MedalShopsStockSaveData MedalShopsPools { get; } = new();

  public BfSerializableCollection<PartyMemberSaveData> PartyMembers { get; } =
    new(Utils.AtSymbolSeparator);

  public BoardQuestsSaveData Quests { get; } = new();
  public BfSerializableCollection<FlagSaveData> RegionalFlags { get; } = new(Utils.CommaSeparator);

  public BfSerializableCollection<BfMusicSaveData> SamiraSongs { get; } =
    new(Utils.AtSymbolSeparator);

  public BfSerializableCollection<StatBonusSaveData> StatBonuses { get; } =
    new(Utils.AtSymbolSeparator);

  public BfSaveData()
  {
    Data = new List<IBfSerializable>
    {
      Header,
      PartyMembers,
      Global,
      MedalShopsAvailables,
      MedalShopsPools,
      Quests,
      Items,
      Medals,
      SamiraSongs,
      StatBonuses,
      Library,
      Flags,
      Flagstrings,
      Flagvars,
      RegionalFlags,
      CrystalBerries,
      Followers,
      EnemyEncounters
    };
  }

  public void LoadFromBytes(byte[] data, IBfSaveFileFormat saveFileFormat)
  {
    string saveData = saveFileFormat.DecodeSaveDataFromSaveFile(data);
    string[] saveSections = saveData.Split('\n');
    for (int i = 0; i < Data.Count; i++)
      Data[i].Deserialize(saveSections[i]);
  }

  public byte[] EncodeToBytes(IBfSaveFileFormat saveFileFormat)
  {
    StringBuilder sb = new();
    for (int i = 0; i < Data.Count; i++)
    {
      sb.Append(Data[i].Serialize());

      if (i != Data.Count - 1)
        sb.Append('\n');
    }

    string saveData = sb.ToString();
    return saveFileFormat.EncodeSaveFilesFromSaveData(saveData);
  }
}
