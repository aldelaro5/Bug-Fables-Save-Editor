using System.IO;

namespace BugFablesLib;

public class BfSwitchSaveDataFormat : IBfSaveFileFormat
{
  private const int SwitchSaveFileSize = 0x100000;

  internal BfSwitchSaveDataFormat() { }

  public string DecodeSaveDataFromSaveFile(byte[] data)
  {
    BinaryReader reader = new(new MemoryStream(data));
    string saveData = reader.ReadString();
    reader.Close();
    return saveData;
  }

  public byte[] EncodeSaveFilesFromSaveData(string saveData)
  {
    byte[] newSave = new byte[SwitchSaveFileSize];
    BinaryWriter writer = new(new MemoryStream(newSave));
    writer.Write(saveData);
    writer.Close();
    return newSave;
  }
}
