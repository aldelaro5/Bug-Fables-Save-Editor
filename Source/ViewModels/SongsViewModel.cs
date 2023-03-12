using BugFablesSaveEditor.BugFablesSave;
using BugFablesSaveEditor.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using static BugFablesSaveEditor.BugFablesSave.Sections.SamiraSongs;

namespace BugFablesSaveEditor.ViewModels;

public partial class SongsViewModel : ObservableObject
{
  [ObservableProperty]
  private SaveData _saveData;

  [ObservableProperty]
  private Song _selectedSongForAdd;

  [ObservableProperty]
  private bool _songForAddIsBought;

  [ObservableProperty]
  private ReorderableCollectionViewModel<SongInfo> _songsVm;

  [ObservableProperty]
  private string[] _songsNames = Utils.GetEnumDescriptions<Song>();

  public SongsViewModel() : this(new SaveData())
  {
    SongsVm.Collection.Add(new SongInfo { Song = (Song)51, IsBought = true });
    SongsVm.Collection.Add(new SongInfo { Song = (Song)67, IsBought = false });
    SongsVm.Collection.Add(new SongInfo { Song = (Song)43, IsBought = true });
  }

  public SongsViewModel(SaveData saveData)
  {
    _saveData = saveData;
    _songsVm = new(_saveData.SamiraSongs.List);
  }

  [RelayCommand]
  private void AddSong()
  {
    SongInfo info = new();
    info.Song = SelectedSongForAdd;
    info.IsBought = SongForAddIsBought;
    SongsVm.Collection.Add(info);
    SongsVm.CollectionView.Refresh();
  }
}
