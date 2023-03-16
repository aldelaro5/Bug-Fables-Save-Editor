using System;
using System.Text;
using BugFablesLib.Data;
using static BugFablesLib.Utils;

namespace BugFablesLib.SaveData;

public sealed class MusicSaveData : IBfData
{
  public const int MusicNotBought = -1;
  public const int MusicBought = 1;

  public bool Bought { get; set; }
  public BfMusic Song { get; set; } = new();

  public void Deserialize(string str)
  {
    string[] data = str.Split(new[] { CommaSeparator }, StringSplitOptions.None);

    Song.Id = ParseValueType<int>(data[0], nameof(Song));
    Bought = ParseValueType<int>(data[1], nameof(Bought)) == MusicBought;
  }

  public string Serialize()
  {
    StringBuilder sb = new();

    sb.Append(Song.Id);
    sb.Append(CommaSeparator);
    sb.Append(Bought ? MusicBought : MusicNotBought);

    return sb.ToString();
  }

  public void ResetToDefault()
  {
    Song.Id = 0;
    Bought = false;
  }
}
