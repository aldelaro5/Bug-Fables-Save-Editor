using System.Text;

namespace BugFablesLib;

public class BfPcSaveData : BfSaveData
{
  public override void LoadFromBytes(byte[] data)
  {
    data = XorData(data);
    base.LoadFromBytes(data);
  }

  public override byte[] EncodeToBytes()
  {
    byte[] data = base.EncodeToBytes();
    return XorData(data);
  }

  private byte[] XorData(byte[] data)
  {
    StringBuilder sb = new(Encoding.UTF8.GetString(data));
    for (int i = 0; i < sb.Length; i++)
      sb[i] ^= (char)543;
    return Encoding.UTF8.GetBytes(sb.ToString());
  }
}
