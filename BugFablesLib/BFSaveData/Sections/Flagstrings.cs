namespace BugFablesLib.BFSaveData.Sections;

public sealed class Flagstrings : BfDataList<Flagstrings.FlagstringInfo>
{
  public Flagstrings()
  {
    ElementSeparator = "|SPLIT|";
  }

  public sealed class FlagstringInfo : BfData
  {
    public string Str { get; set; } = "";

    public override void ResetToDefault()
    {
      Str = string.Empty;
    }

    public override void Parse(string str)
    {
      Str = str;
    }

    public override string ToString()
    {
      return Str;
    }
  }
}
