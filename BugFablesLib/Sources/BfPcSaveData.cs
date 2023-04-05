using System.Text;

namespace BugFablesLib;

public class BfPcSaveData : BfSaveData
{
  public override void LoadFromString(string data)
  {
    data = XorData(data);
    base.LoadFromString(data);
  }

  public override string EncodeToString()
  {
    string data = base.EncodeToString();
    return XorData(data);
  }

  private string XorData(string data)
  {
    StringBuilder sb = new(data);
    for (int i = 0; i < sb.Length; i++)
      sb[i] ^= (char)543;
    return sb.ToString();
  }
}
