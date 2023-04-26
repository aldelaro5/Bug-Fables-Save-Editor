using System.IO;
using System.Threading.Tasks;

namespace BugFablesLib;

public class BfSwitchSaveDataFormat : IBfSaveFileFormat
{
  private const int SwitchSaveFileSize = 0x100000;

  internal BfSwitchSaveDataFormat() { }

  public Task<string> DecodeSaveDataFromSaveFile(byte[] data)
  {
    BinaryReader reader = new(new MemoryStream(data));
    string saveData = reader.ReadString();
    reader.Close();
    return Task.FromResult(saveData);
  }

  public Task<byte[]> EncodeSaveFilesFromSaveData(string saveData)
  {
    byte[] newSave = new byte[SwitchSaveFileSize];
    BinaryWriter writer = new(new MemoryStream(newSave));
    writer.Write(saveData);
    writer.Close();
    return Task.FromResult(newSave);
  }
}
