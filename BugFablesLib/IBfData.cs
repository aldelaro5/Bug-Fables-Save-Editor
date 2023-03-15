namespace BugFablesLib;

public interface IBfData
{
  public void ResetToDefault();
  public void Deserialize(string str);
  public string Serialize();
}

public abstract class BfData : IBfData
{
  public abstract void ResetToDefault();
  public abstract void Deserialize(string str);
  public abstract string Serialize();
  public override string ToString() => Serialize();
}
