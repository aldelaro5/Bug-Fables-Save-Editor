namespace BugFablesLib.BFSaveData.Sections;

public sealed class RegionalFlags : BfDataList<RegionalFlags.RegionalInfo>
{
  public sealed class RegionalInfo : BfData
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
