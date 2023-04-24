using System.Text;

namespace BugFablesLib;

public class BfPcSaveDataFormat : IBfSaveFileFormat
{
  internal BfPcSaveDataFormat() { }

  public string DecodeSaveDataFromSaveFile(byte[] data) => XorData(Encoding.UTF8.GetString(data));
  public byte[] EncodeSaveFilesFromSaveData(string saveData) => Encoding.UTF8.GetBytes(XorData(saveData));

  private string XorData(string data)
  {
    StringBuilder sb = new(data);
    for (int i = 0; i < sb.Length; i++)
      sb[i] ^= (char)543;
    return sb.ToString();
  }
}
