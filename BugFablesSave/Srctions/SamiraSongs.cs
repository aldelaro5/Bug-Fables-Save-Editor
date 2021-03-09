using BugFablesSaveEditor.BugFablesEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class SamiraSongs : IBugFablesSaveSection
  {
    private const int songNotBought = -1;
    private const int songBought = 1;

    public class AvailableSong
    {
      public Song Song { get; set; }
      public bool IsBought { get; set; }
    }

    public object Data { get; set; } = new List<AvailableSong>();

    public void ParseFromSaveLine(string saveLine)
    {
      string[] songsData = saveLine.Split(Common.ElementSeparator);
      List<AvailableSong> songs = (List<AvailableSong>)Data;

      for (int i = 0; i < songsData.Length; i++)
      {
        if (songsData[i] == string.Empty)
          continue;

        string[] data = songsData[i].Split(Common.FieldSeparator);

        AvailableSong newSong = new AvailableSong();

        int intOut = 0;
        if (!int.TryParse(data[0], out intOut))
          throw new Exception(nameof(SamiraSongs) + "[" + i + "]." + nameof(AvailableSong.Song) + " failed to parse");
        if (intOut < 0 || intOut >= (int)Song.COUNT)
          throw new Exception(nameof(SamiraSongs) + "[" + i + "]." + nameof(AvailableSong.Song) + ": " + intOut + " is not a valid song ID");
        newSong.Song = (Song)intOut;
        if (!int.TryParse(data[1], out intOut))
          throw new Exception(nameof(SamiraSongs) + "[" + i + "]." + nameof(AvailableSong.IsBought) + " failed to parse");
        if (intOut != songNotBought && intOut != songBought)
          throw new Exception(nameof(SamiraSongs) + "[" + i + "]." + nameof(AvailableSong.IsBought) + ": " + intOut + " is not a valid song availability value");
        newSong.IsBought = intOut == songBought;

        songs.Add(newSong);
      }
    }

    public string EncodeToSaveLine()
    {
      List<AvailableSong> songs = (List<AvailableSong>)Data;
      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < songs.Count; i++)
      {
        sb.Append((int)songs[i].Song);
        sb.Append(Common.FieldSeparator);
        sb.Append(songs[i].IsBought ? songBought : songNotBought);

        if (i != songs.Count - 1)
          sb.Append(Common.ElementSeparator);
      }

      return sb.ToString();
    }
  }
}
