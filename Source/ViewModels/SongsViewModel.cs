using System.Collections.ObjectModel;
using System.Reactive;
using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave;
using ReactiveUI;
using static BugFablesSaveEditor.BugFablesSave.Sections.SamiraSongs;

namespace BugFablesSaveEditor.ViewModels;

public class SongsViewModel : ViewModelBase
{
  private SaveData _saveData;

  private SongInfo _selectedSong;

  private Song _selectedSongForAdd;

  private ObservableCollection<SongInfo> _songs;

  private string[] _songsNames;

  public SongsViewModel()
  {
    SaveData = new SaveData();
    Initialize();

    Songs.Add(new SongInfo { Song = (Song)51, IsBought = true });
    Songs.Add(new SongInfo { Song = (Song)67, IsBought = false });
    Songs.Add(new SongInfo { Song = (Song)43, IsBought = true });
  }

  public SongsViewModel(SaveData saveData)
  {
    SaveData = saveData;
    Initialize();
  }

  public SaveData SaveData
  {
    get => _saveData;
    set
    {
      _saveData = value;
      this.RaisePropertyChanged();
    }
  }

  public string[] SongsNames
  {
    get => _songsNames;
    set
    {
      _songsNames = value;
      this.RaisePropertyChanged();
    }
  }

  public Song SelectedSongForAdd
  {
    get => _selectedSongForAdd;
    set
    {
      _selectedSongForAdd = value;
      this.RaisePropertyChanged();
    }
  }

  public bool SongForAddIsBought { get; set; }

  public SongInfo SelectedSong
  {
    get => _selectedSong;
    set
    {
      _selectedSong = value;
      this.RaisePropertyChanged();
    }
  }

  public ObservableCollection<SongInfo> Songs
  {
    get => _songs;
    set
    {
      _songs = value;
      this.RaisePropertyChanged();
    }
  }

  public ReactiveCommand<Unit, Unit> CmdReorderSongsUp { get; set; }
  public ReactiveCommand<Unit, Unit> CmdReorderSongsDown { get; set; }

  private void Initialize()
  {
    SongsNames = Common.GetEnumDescriptions<Song>();
    Songs = (ObservableCollection<SongInfo>)SaveData.Sections[SaveFileSection.SamiraSongs].Data;

    CmdReorderSongsUp = ReactiveCommand.Create(() =>
    {
      ReorderSong(ReorderDirection.Up);
    }, this.WhenAnyValue(x => x.SelectedSong, x => x != null && Songs[0] != x));
    CmdReorderSongsDown = ReactiveCommand.Create(() =>
    {
      ReorderSong(ReorderDirection.Down);
    }, this.WhenAnyValue(x => x.SelectedSong, x => x != null && Songs[Songs.Count - 1] != x));
  }

  private void ReorderSong(ReorderDirection direction)
  {
    int index = Songs.IndexOf(SelectedSong);
    int newIndex = index;
    if (direction == ReorderDirection.Up)
    {
      newIndex--;
    }
    else if (direction == ReorderDirection.Down)
    {
      newIndex++;
    }

    Song song = SelectedSong.Song;
    bool isBought = SelectedSong.IsBought;
    Songs.Remove(SelectedSong);
    Songs.Insert(newIndex, new SongInfo { Song = song, IsBought = isBought });
    SelectedSong = Songs[newIndex];
  }

  public void RemoveSong(SongInfo songInfo)
  {
    Songs.Remove(songInfo);
  }

  public void AddSong()
  {
    Songs.Add(new SongInfo { Song = SelectedSongForAdd, IsBought = SongForAddIsBought });
  }
}
