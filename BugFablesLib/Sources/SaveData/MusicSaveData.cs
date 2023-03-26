using System;
using System.Text;
using BugFablesLib.Data;

namespace BugFablesLib.SaveData;

public sealed class MusicSaveData : IBfSerializable
{
  private const int MusicNotBought = -1;
  private const int MusicBought = 1;

  public bool Bought { get; set; }
  public BfMusic Song { get; set; } = new();

  public void Deserialize(string str)
  {
    string[] data = str.Split(new[] { Utils.CommaSeparator }, StringSplitOptions.None);

    Song.Id = Utils.ParseValueType<int>(data[0], nameof(Song));
    Bought = Utils.ParseValueType<int>(data[1], nameof(Bought)) == MusicBought;
  }

  public string Serialize()
  {
    StringBuilder sb = new();

    sb.Append(Song.Id);
    sb.Append(Utils.CommaSeparator);
    sb.Append(Bought ? MusicBought : MusicNotBought);

    return sb.ToString();
  }

  public void ResetToDefault()
  {
    Song.Id = 0;
    Bought = false;
  }
}
