using System.Text;
using System.Threading.Tasks;

namespace BugFablesLib;

public class BfPcSaveDataFormat : IBfSaveFileFormat
{
  internal BfPcSaveDataFormat() { }

  public Task<string> DecodeSaveDataFromSaveFile(byte[] data) =>
    Task.FromResult(XorData(Encoding.UTF8.GetString(data)));

  public Task<byte[]> EncodeSaveFilesFromSaveData(string saveData) =>
    Task.FromResult(Encoding.UTF8.GetBytes(XorData(saveData)));

  private string XorData(string data)
  {
    StringBuilder sb = new(data);
    for (int i = 0; i < sb.Length; i++)
      sb[i] ^= (char)543;
    return sb.ToString();
  }
}
