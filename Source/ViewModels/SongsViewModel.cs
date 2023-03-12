using System.Collections.Generic;
using BugFablesSaveEditor.BugFablesSave;
using BugFablesSaveEditor.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using static BugFablesSaveEditor.BugFablesSave.Sections.SamiraSongs;

namespace BugFablesSaveEditor.ViewModels;

public partial class SongsViewModel : ObservableObject
{
  [ObservableProperty]
  private SaveData _saveData = null!;

  [ObservableProperty]
  private Song _selectedSongForAdd;

  [ObservableProperty]
  private bool _songForAddIsBought;

  [ObservableProperty]
  private ReorderableCollectionViewModel<SongInfo> _songsVm = null!;

  [ObservableProperty]
  private string[] _songsNames = null!;

  public SongsViewModel() : this(new SaveData())
  {
    SongsVm.Collection.Add(new SongInfo { Song = (Song)51, IsBought = true });
    SongsVm.Collection.Add(new SongInfo { Song = (Song)67, IsBought = false });
    SongsVm.Collection.Add(new SongInfo { Song = (Song)43, IsBought = true });
  }

  public SongsViewModel(SaveData saveData)
  {
    SaveData = saveData;
    SongsNames = Utils.GetEnumDescriptions<Song>();
    SongsVm = new ReorderableCollectionViewModel<SongInfo>(SaveData.SamiraSongs.List);
  }

  [RelayCommand]
  private void AddSong()
  {
    SongsVm.Collection.Add(
      new SongInfo { Song = SelectedSongForAdd, IsBought = SongForAddIsBought });
  }
}
