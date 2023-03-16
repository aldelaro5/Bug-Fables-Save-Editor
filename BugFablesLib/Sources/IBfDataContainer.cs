using System.Collections.Generic;

namespace BugFablesLib;

public interface IBfDataContainer
{
  public IList<IBfData> Data { get; }
  public void LoadFromBytes(byte[] data);
  public byte[] EncodeToBytes();
  public void ResetToDefault();
}
