namespace BugFablesLib;

public interface IBfData
{
  public void ResetToDefault();
  public void Deserialize(string str);
  public string Serialize();
}
