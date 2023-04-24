using System.Text;

namespace BugFablesLib;

public class BfPcSaveData : BfSaveData
{
  public override void LoadFromBytes(byte[] data)
  {
    string deXored = XorData(Encoding.UTF8.GetString(data));
    LoadFromString(deXored);
  }

  public override byte[] EncodeToBytes()
  {
    string data = EncodeToString();
    return Encoding.UTF8.GetBytes(XorData(data));
  }

  private string XorData(string data)
  {
    StringBuilder sb = new(data);
    for (int i = 0; i < sb.Length; i++)
      sb[i] ^= (char)543;
    return sb.ToString();
  }
}
