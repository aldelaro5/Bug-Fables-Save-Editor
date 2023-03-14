namespace BugFablesLib.BFSaveData.Sections;

public sealed class Flagvars : BfDataList<Flagvars.FlagvarInfo>
{
  public sealed class FlagvarInfo : BfData
  {
    public int Var { get; set; }

    public override void ResetToDefault()
    {
      Var = 0;
    }

    public override void Parse(string str)
    {
      Var = ParseField<int>(str, nameof(Var));
    }

    public override string ToString()
    {
      return Var.ToString();
    }
  }
}
