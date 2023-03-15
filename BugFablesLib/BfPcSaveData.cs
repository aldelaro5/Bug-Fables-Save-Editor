using System.Text;

namespace BugFablesLib;

public class BfPcSaveData : BfSaveData
{
  public override void LoadFromString(string data)
  {
    StringBuilder sb = new(data);
    for (int i = 0; i < sb.Length; i++)
      sb[i] = (char)(sb[i] ^ 543);
    base.LoadFromString(sb.ToString());
  }

  public override string EncodeToString()
  {
    StringBuilder sb = new(base.EncodeToString());
    for (int i = 0; i < sb.Length; i++)
      sb[i] = (char)(sb[i] ^ 543);
    return sb.ToString();
  }
}
