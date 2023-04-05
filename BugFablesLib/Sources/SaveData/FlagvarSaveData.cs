namespace BugFablesLib.SaveData;

public sealed class FlagvarSaveData : IBfSerializable
{
  public int Var { get; set; }
  public void Deserialize(string str) => Var = Utils.ParseValueType<int>(str, nameof(Var));
  public string Serialize() => Var.ToString();
  public void ResetToDefault() => Var = 0;
}
