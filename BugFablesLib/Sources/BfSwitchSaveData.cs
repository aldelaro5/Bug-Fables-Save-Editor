using System.IO;

namespace BugFablesLib;

public class BfSwitchSaveData : BfSaveData
{
  private const int SwitchSaveFileSize = 0x100000;

  public override void LoadFromBytes(byte[] data)
  {
    BinaryReader reader = new(new MemoryStream(data));
    LoadFromString(reader.ReadString());
    reader.Close();
  }

  public override byte[] EncodeToBytes()
  {
    string data = EncodeToString();
    byte[] newSave = new byte[SwitchSaveFileSize];
    BinaryWriter writer = new(new MemoryStream(newSave));
    writer.Write(data);
    writer.Close();
    return newSave;
  }
}
