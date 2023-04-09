namespace BugFablesLib;

public interface IBfSerializable
{
  public void Deserialize(string str);
  public string Serialize();
}
