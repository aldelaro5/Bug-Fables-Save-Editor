using BugFablesSaveEditor.BugFablesEnums;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace BugFablesSaveEditor.BugFablesSave.Sections
{
  public class SamiraSongs : IBugFablesSaveSection
  {
    private const int songNotBought = -1;
    private const int songBought = 1;

    public class SongInfo : INotifyPropertyChanged
    {
      private Song _song;
      public Song Song
      {
        get { return _song; }
        set
        {
          if ((int)value == -1)
            return;

          _song = value;
          NotifyPropertyChanged();
        }
      }

      private bool _isBought;
      public bool IsBought { get { return _isBought; } set { _isBought = value; NotifyPropertyChanged(); } }

      public event PropertyChangedEventHandler? PropertyChanged;
      private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
      {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    public object Data { get; set; } = new ObservableCollection<SongInfo>();

    public void ParseFromSaveLine(string saveLine)
    {
      string[] songsData = saveLine.Split(CommonUtils.ElementSeparator);
      ObservableCollection<SongInfo> songs = (ObservableCollection<SongInfo>)Data;

      for (int i = 0; i < songsData.Length; i++)
      {
        if (songsData[i] == string.Empty)
          continue;

        string[] data = songsData[i].Split(CommonUtils.FieldSeparator);

        SongInfo newSong = new SongInfo();

        int intOut = 0;
        if (!int.TryParse(data[0], out intOut))
          throw new Exception(nameof(SamiraSongs) + "[" + i + "]." + nameof(SongInfo.Song) + " failed to parse");
        if (intOut < 0 || intOut >= (int)Song.COUNT)
          throw new Exception(nameof(SamiraSongs) + "[" + i + "]." + nameof(SongInfo.Song) + ": " + intOut + " is not a valid song ID");
        newSong.Song = (Song)intOut;
        if (!int.TryParse(data[1], out intOut))
          throw new Exception(nameof(SamiraSongs) + "[" + i + "]." + nameof(SongInfo.IsBought) + " failed to parse");
        if (intOut != songNotBought && intOut != songBought)
          throw new Exception(nameof(SamiraSongs) + "[" + i + "]." + nameof(SongInfo.IsBought) + ": " + intOut + " is not a valid song availability value");
        newSong.IsBought = intOut == songBought;

        songs.Add(newSong);
      }
    }

    public string EncodeToSaveLine()
    {
      ObservableCollection<SongInfo> songs = (ObservableCollection<SongInfo>)Data;
      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < songs.Count; i++)
      {
        sb.Append((int)songs[i].Song);
        sb.Append(CommonUtils.FieldSeparator);
        sb.Append(songs[i].IsBought ? songBought : songNotBought);

        if (i != songs.Count - 1)
          sb.Append(CommonUtils.ElementSeparator);
      }

      return sb.ToString();
    }

    public void ResetToDefault()
    {
      ObservableCollection<SongInfo> songs = (ObservableCollection<SongInfo>)Data;
      songs.Clear();
    }
  }
}
