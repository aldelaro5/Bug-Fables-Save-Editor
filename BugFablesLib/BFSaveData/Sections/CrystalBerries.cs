namespace BugFablesLib.BFSaveData.Sections;

public sealed class CrystalBerries : BfDataList<CrystalBerries.CrystalBerry>
{
  public sealed class CrystalBerry : BfData
  {
    public bool Obtained { get; set; }

    public override void ResetToDefault()
    {
      Obtained = false;
    }

    public override void Parse(string str)
    {
      Obtained = ParseField<bool>(str, nameof(Obtained));
    }

    public override string ToString()
    {
      return Obtained.ToString();
    }
  }
}
