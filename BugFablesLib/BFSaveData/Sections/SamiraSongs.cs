using System;
using System.Text;

namespace BugFablesLib.BFSaveData.Sections;

public sealed class SamiraSongs : BfDataList<SamiraSongs.SongInfo>
{
  public const int SongNotBought = -1;
  public const int SongBought = 1;

  public SamiraSongs()
  {
    ElementSeparator = Utils.SecondarySeparator;
  }

  public sealed class SongInfo : BfData
  {
    public bool Bought { get; set; }
    public int Song { get; set; }

    public override void ResetToDefault()
    {
      Song = 0;
      Bought = false;
    }

    public override void Parse(string str)
    {
      string[] data = str.Split(new[] {Utils.PrimarySeparator}, StringSplitOptions.None);

      Song = ParseField<int>(data[0], nameof(Song));
      Bought = ParseField<int>(data[1], nameof(Bought)) == SongBought;
    }

    public override string ToString()
    {
      StringBuilder sb = new();

      sb.Append(Song);
      sb.Append(Utils.PrimarySeparator);
      sb.Append(Bought ? SongBought : SongNotBought);

      return sb.ToString();
    }
  }
}
