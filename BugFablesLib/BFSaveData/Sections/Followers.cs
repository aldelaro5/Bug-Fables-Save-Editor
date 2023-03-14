namespace BugFablesLib.BFSaveData.Sections;

public sealed class Followers : BfDataList<Followers.FollowerInfo>
{
  public sealed class FollowerInfo : BfData
  {
    public int AnimId { get; set; }

    public override void ResetToDefault()
    {
      AnimId = 0;
    }

    public override void Parse(string str)
    {
      AnimId = ParseField<int>(str, nameof(AnimId));
    }

    public override string ToString()
    {
      return AnimId.ToString();
    }
  }
}
