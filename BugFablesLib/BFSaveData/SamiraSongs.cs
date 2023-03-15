using System;
using System.Text;
using static BugFablesLib.Utils;

namespace BugFablesLib.BFSaveData;

public sealed class Music : BfData
{
  public const int MusicNotBought = -1;
  public const int MusicBought = 1;

  public bool Bought { get; set; }
  public int Song { get; set; }

  public override void Deserialize(string str)
  {
    string[] data = str.Split(new[] { CommaSeparator }, StringSplitOptions.None);

    Song = ParseValueType<int>(data[0], nameof(Song));
    Bought = ParseValueType<int>(data[1], nameof(Bought)) == MusicBought;
  }

  public override string Serialize()
  {
    StringBuilder sb = new();

    sb.Append(Song);
    sb.Append(CommaSeparator);
    sb.Append(Bought ? MusicBought : MusicNotBought);

    return sb.ToString();
  }

  public override void ResetToDefault()
  {
    Song = 0;
    Bought = false;
  }
}
