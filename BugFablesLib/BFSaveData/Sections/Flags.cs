namespace BugFablesLib.BFSaveData.Sections;

public sealed class Flags : BfDataList<Flags.FlagInfo>
{
  public sealed class FlagInfo : BfData
  {
    public bool Enabled { get; set; }

    public override void ResetToDefault()
    {
      Enabled = false;
    }

    public override void Parse(string str)
    {
      Enabled = ParseField<bool>(str, nameof(Enabled));
    }

    public override string ToString()
    {
      return Enabled.ToString();
    }
  }
}
