using System.Collections.ObjectModel;
using BugFablesSaveEditor.BugFablesEnums;
using BugFablesSaveEditor.BugFablesSave;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using static BugFablesSaveEditor.BugFablesSave.Sections.SamiraSongs;

namespace BugFablesSaveEditor.ViewModels;

public partial class SongsViewModel : ObservableObject
{
  [ObservableProperty]
  private SaveData _saveData = null!;

  [ObservableProperty]
  [NotifyCanExecuteChangedFor(nameof(CmdReorderSongsUpCommand))]
  [NotifyCanExecuteChangedFor(nameof(CmdReorderSongsDownCommand))]
  private SongInfo? _selectedSong;

  [ObservableProperty]
  private Song _selectedSongForAdd;

  [ObservableProperty] private bool _songForAddIsBought;

  [ObservableProperty]
  private ObservableCollection<SongInfo> _songs = null!;

  [ObservableProperty]
  private string[] _songsNames = null!;

  public SongsViewModel() : this(new SaveData())
  {
    Songs.Add(new SongInfo { Song = (Song)51, IsBought = true });
    Songs.Add(new SongInfo { Song = (Song)67, IsBought = false });
    Songs.Add(new SongInfo { Song = (Song)43, IsBought = true });
  }

  public SongsViewModel(SaveData saveData)
  {
    SaveData = saveData;
    SongsNames = Common.GetEnumDescriptions<Song>();
    Songs = (ObservableCollection<SongInfo>)SaveData.Sections[SaveFileSection.SamiraSongs].Data;
  }

  [RelayCommand(CanExecute = nameof(CanReorderSongsUp))]
  private void CmdReorderSongsUp()
  {
    ReorderSong(ReorderDirection.Up);
  }

  private bool CanReorderSongsUp()
  {
    return Songs.Count > 0 && SelectedSong is not null && Songs[0] != SelectedSong;
  }

  [RelayCommand(CanExecute = nameof(CanReorderSongsDown))]
  private void CmdReorderSongsDown()
  {
    ReorderSong(ReorderDirection.Down);
  }
  private bool CanReorderSongsDown()
  {
    return Songs.Count > 0 && SelectedSong is not null && Songs[^1] != SelectedSong;
  }

  private void ReorderSong(ReorderDirection direction)
  {
    int index = Songs.IndexOf(SelectedSong);
    int newIndex = index;
    if (direction == ReorderDirection.Up)
      newIndex--;
    else if (direction == ReorderDirection.Down)
      newIndex++;

    Song song = SelectedSong.Song;
    bool isBought = SelectedSong.IsBought;
    Songs.Remove(SelectedSong);
    Songs.Insert(newIndex, new SongInfo { Song = song, IsBought = isBought });
    SelectedSong = Songs[newIndex];
  }

  [RelayCommand]
  private void RemoveSong(SongInfo songInfo)
  {
    Songs.Remove(songInfo);
  }

  [RelayCommand]
  private void AddSong()
  {
    Songs.Add(new SongInfo { Song = SelectedSongForAdd, IsBought = SongForAddIsBought });
  }
}
