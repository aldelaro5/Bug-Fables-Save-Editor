namespace BugFablesLib;

public interface IBfSerializable
{
  public void ResetToDefault();
  public void Deserialize(string str);
  public string Serialize();
}
